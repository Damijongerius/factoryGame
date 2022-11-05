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
// OnSaveRequest
// // \\ // \\ // \\
app.post("/Save/savefile", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const saveFile = SaveFile_1.Convert.toSaveFile(req.body.sendJson);
        console.log(saveFile);
        const { map, profile } = saveFile;
        const result = yield Database_1.DB.insert.SaveFile(saveFile, req.body.GUID);
        console.log(result);
        Database_1.DB.insert.Profile(profile, result);
        Database_1.DB.insert.statistics(profile.Statistics, result);
        Database_1.DB.insert.Map(map, result);
        map.grid.forEach((element) => {
            Database_1.DB.insert.Cell(element, result);
            Database_1.DB.insert.Objinfo(element.ObjInfo, element.x, element.y, result);
        });
    });
});
// \\ // \\ // \\ //
// onloadProfileRequest
// // \\ // \\ // \\
app.post("/load/profiles", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        if (req.body.GUID !== null) {
            const result = yield Database_1.DB.select.SaveFile(req.body.GUID);
            if (result !== null) {
                const respond = new Array();
                for (const rs of result) {
                    const profiles = yield Database_1.DB.select.Profile(rs.ID);
                    respond.push(profiles[0]);
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
        if (GUID !== null) {
            if (ID instanceof (Array)) {
                for (const sfid of ID) {
                    const sf = yield GenerateSaveFile(sfid);
                }
            }
            else if (ID !== null) {
                const sf = yield GenerateSaveFile(ID);
                res.send(sf);
            }
            else {
                res.send({ status: 13, message: "need valid ID" });
            }
        }
        else {
            res.send({ status: 11, message: "need valid GUID" });
        }
        function GenerateSaveFile(ID) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("generating sf");
                const sf = yield Database_1.DB.select.SaveFile(ID);
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
                    networth: nstatistics[0].networth,
                    money: nstatistics[0].money,
                    data: nstatistics[0].data,
                    xp: nstatistics[0].xp,
                    Level: nstatistics[0].Level,
                };
                profile.Statistics = statistics;
                const ncells = yield Database_1.DB.select.Cell(ID);
                for (const idx in ncells) {
                    const cells = {
                        x: ncells[idx].x,
                        y: ncells[idx].y,
                        objType: ncells[idx].ObjInfo,
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
        Database_1.DB.select.User(data, function (info) {
            return __awaiter(this, void 0, void 0, function* () {
                switch (info.status) {
                    case 3: {
                        res.send({
                            Status: 3,
                            message: `no existing user with name ${data.UserName}`,
                        });
                        break;
                    }
                    case 2: {
                        info.result.forEach(function (i, idx, array) {
                            return __awaiter(this, void 0, void 0, function* () {
                                comparePassword(req.body.Password, array[idx].password, function (params) {
                                    if (params) {
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
                        res.send({
                            Status: 101,
                            message: "error on our end",
                        });
                        break;
                    }
                }
            });
        });
    });
});
// \\ // \\ // \\ //
//onUserDeleteRequest
// // \\ // \\ // \\
app.post("/DeleteUser", function (req, res) {
    //removes player with savefiles
    //deletes profile with saved guid
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