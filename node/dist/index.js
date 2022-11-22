"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
// // \\ // \\ // \\
const SaveFile_1 = require("./models/SaveFile");
const Database_1 = require("./DB/Database");
const express_1 = __importDefault(require("express"));
const bcrypt_1 = __importDefault(require("bcrypt"));
const body_parser_1 = __importDefault(require("body-parser"));
// \\ // \\ // \\ //
// // \\ // \\ // \\
const app = (0, express_1.default)();
app.use(express_1.default.json());
app.use(body_parser_1.default.urlencoded({ extended: false }));
app.use(body_parser_1.default.json());
// \\ // \\ // \\ //
// onApplication start
// // \\ // \\ // \\
app.listen(3000, function () {
    console.log("server running");
});
// \\ // \\ // \\ //
// OnSaveSaveFileRequest
// // \\ // \\ // \\
app.post("/Save/savefile", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const saveFile = SaveFile_1.Convert.toSaveFile(req.body.sendJson);
        const { map, profile } = saveFile;
        const savedFile = yield Database_1.DB.select.sf(req.body.GUID, saveFile.profile.Name);
        console.log(savedFile);
        if (savedFile.length == 0) {
            const result = yield Database_1.DB.insert.SaveFile(saveFile, req.body.GUID);
            console.log(result);
            Database_1.DB.insert.Profile(profile, result);
            Database_1.DB.insert.statistics(profile.Statistics, result);
            Database_1.DB.insert.Map(map, result);
            map.grid.forEach((element) => {
                Database_1.DB.insert.Cell(element, result);
                Database_1.DB.insert.Objinfo(element.ObjInfo, element.x, element.y, result);
            });
        }
        else {
            const { ID } = savedFile[0];
            Database_1.DB.update.Profile(profile, ID);
            Database_1.DB.update.statistics(profile.Statistics, ID);
            const savedCells = yield Database_1.DB.select.Cell(ID);
            const copy = saveFile.map.grid;
            for (const cell of savedCells) {
                for (const cc of saveFile.map.grid) {
                    if (cc.x == cell.x && cell.y == cc.y) {
                        Database_1.DB.update.Cell(cc, ID);
                        Database_1.DB.update.Objinfo(cc.ObjInfo, cc.x, cc.y, ID);
                        const index = copy.indexOf(cc);
                        console.log(cc);
                        copy.splice(index);
                    }
                }
            }
            for (const cell of copy) {
                console.log(cell);
                Database_1.DB.insert.Cell(cell, ID);
                Database_1.DB.insert.Objinfo(cell.ObjInfo, cell.x, cell.y, ID);
            }
        }
    });
});
// \\ // \\ // \\ //
// onloadProfileRequest
// // \\ // \\ // \\
app.post("/load/profiles", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        if (req.body.GUID !== null) {
            const result = yield Database_1.DB.select.SaveFiles(req.body.GUID);
            console.log(result);
            if (result !== null) {
                const respond = new Array();
                for (const rs of result) {
                    const profiles = yield Database_1.DB.select.Profile(rs.ID);
                    respond.push(profiles[0]);
                    console.log(profiles);
                }
                res.send({ profiles: respond, saveFiles: result });
            }
            else {
                res.send({ status: 101, message: "unknown ERROR" });
            }
        }
        else {
            res.send({ status: 11, message: "need valid GUID" });
        }
    });
});
// \\ // \\ // \\ //
// onloadSaveFilesRequest
// // \\ // \\ // \\
app.post("/Load/savefile", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const ID = req.body.ID;
        const GUID = req.body.GUID;
        if (ID != null) {
            const sf = yield GenerateSaveFile(ID, GUID);
            console.log(sf);
            res.json(sf);
        }
        else {
            res.send({ status: 13, message: "need valid ID" });
        }
        function GenerateSaveFile(ID, GUID) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("generating sf");
                const sf = yield Database_1.DB.select.SaveFile(ID, GUID);
                const nprofile = yield Database_1.DB.select.Profile(ID);
                const profile = {
                    Name: sf[0].SaveName,
                    DateMade: nprofile[0].DateMade,
                    DateSeen: nprofile[0].DateSeen,
                    TimePlayed: nprofile[0].TimePlayed,
                    Statistics: null,
                };
                const nmap = yield Database_1.DB.select.Map(ID);
                const map = {
                    xRange: nmap[0].xRange,
                    yRange: nmap[0].yRange,
                    grid: [],
                };
                const nstatistics = yield Database_1.DB.select.statistics(ID);
                const statistics = {
                    networth: nstatistics[0].Networth,
                    money: nstatistics[0].Money,
                    data: nstatistics[0].Data,
                    xp: nstatistics[0].Xp,
                    Level: nstatistics[0].Level,
                };
                profile.Statistics = statistics;
                const ncells = yield Database_1.DB.select.Cell(ID);
                console.log(ncells);
                for (const idx in ncells) {
                    console.log(ncells[idx].ObjectTypes);
                    const cells = {
                        x: ncells[idx].x,
                        y: ncells[idx].y,
                        objType: ncells[idx].ObjectTypes,
                        ObjInfo: null,
                    };
                    const nObjInfo = yield Database_1.DB.select.Objinfo(ncells[idx].x, ncells[idx].y, ID);
                    const ObjInfo = {
                        exitPoints: nObjInfo[0].exitPoints,
                        powered: nObjInfo[0].powered,
                        dataStored: nObjInfo[0].dataStored,
                        powerStored: nObjInfo[0].powerStored,
                        level: nObjInfo[0].level,
                        age: nObjInfo[0].age,
                        upkeepCost: nObjInfo[0].upkeepCost,
                        dataMined: nObjInfo[0].dataMined,
                        dataSold: nObjInfo[0].dataSold,
                        dataTransferd: nObjInfo[0].dataTransferd,
                        Prio: nObjInfo[0].Prio,
                        SelfPrio: nObjInfo[0].SelfPrio,
                        updateSpeed: nObjInfo[0].updateSpeed,
                    };
                    cells.ObjInfo = ObjInfo;
                    map.grid.push(cells);
                }
                const saveFile = { map, profile };
                return saveFile;
            });
        }
    });
});
// \\ // \\ // \\ //
//onUserCreateRequest
// // \\ // \\ // \\
app.post("/CreateUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const asyncHash = yield EncryptPasswordASync(req.body.Password);
        Database_1.DB.insert.User(req.body.GUID, req.body.UserName, asyncHash, function (info) {
            res.send(JSON.stringify(info));
        });
    });
});
// \\ // \\ // \\ //
//onUserLoadRequest
// // \\ // \\ // \\
app.post("/LoadUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        //needs guid username and password
        const data = {
            UserName: req.body.UserName,
            Password: req.body.Password,
        };
        if (req.body.GUID !== null) {
            data.GUID = req.body.GUID;
        }
        const info = yield Database_1.DB.select.User(data);
        switch (info.status) {
            case 3: {
                res.send({
                    Status: 3,
                    message: `no existing user with name ${data.UserName}`,
                });
                break;
            }
            case 2: {
                if (info.result.length == 0 || info.result == null) {
                    console.log("nothing");
                    res.send({
                        Status: 5,
                        message: "incorrect password try again",
                    });
                }
                info.result.forEach(function (i, idx, array) {
                    return __awaiter(this, void 0, void 0, function* () {
                        comparePassword(req.body.Password, array[idx].password, function (params) {
                            if (params) {
                                console.log("match");
                                res.send({
                                    Status: 4,
                                    message: `the password matches ${data.UserName}`,
                                    Info: {
                                        UserName: array[idx].UserName,
                                        GUID: array[idx].UserId,
                                    },
                                });
                            }
                            else {
                                if (i === array.length - 1) {
                                    console.log("not match");
                                    res.send({
                                        Status: 5,
                                        message: "incorrect password try again",
                                    });
                                }
                            }
                        });
                    });
                });
                break;
            }
            default: {
                console.log("error");
                res.send({
                    Status: 101,
                    message: "error on our end",
                });
                break;
            }
        }
    });
});
// \\ // \\ // \\ //
//onUserDeleteRequest
// // \\ // \\ // \\
app.post("/DeleteUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        console.log("delete");
        if (req.body.GUID != null) {
            Database_1.DB.delete.User(req.body.GUID);
        }
    });
});
// \\ // \\ // \\ //
//onSaveFileDeleteRequest
// // \\ // \\ // \\
app.post("/DeleteSaveFile", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        if (req.body.GUID != null && req.body.saveName != null) {
            Database_1.DB.delete.SaveFile(req.body.GUID, req.body.saveName);
        }
    });
});
// \\ // \\ // \\ //
// encryption
// // \\ // \\ // \\
function EncryptPasswordASync(password) {
    return __awaiter(this, void 0, void 0, function* () {
        const hash = yield bcrypt_1.default.hash(password, 10);
        return hash;
    });
}
function comparePassword(password, hash, callback) {
    return __awaiter(this, void 0, void 0, function* () {
        callback(yield bcrypt_1.default.compare(password, hash));
    });
}
// \\ // \\ // \\ //
//# sourceMappingURL=index.js.map