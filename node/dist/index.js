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
Object.defineProperty(exports, "__esModule", { value: true });
const SaveFile_1 = require("./models/SaveFile");
const Database_1 = require("./DB/Database");
const { saveFile } = require("./models/SaveFile");
//node module express
const { response, request, json } = require("express");
const express = require("express");
const bcrypt = require("bcrypt");
//node module body parser
const bodyParser = require("body-parser");
const app = express();
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.listen(3000, function () {
    console.log("server running");
});
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
        map.grid.forEach((element, index, array) => {
            Database_1.DB.insert.Cell(element, result);
            Database_1.DB.insert.Objinfo(element.ObjInfo, element.x, element.y, result);
        });
    });
});
// \\ // \\ // \\ //
app.post("/load/profiles", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        if (req.body.GUID != null) {
            const result = yield Database_1.DB.select.SaveFile(req.body.GUID);
            if (result != null) {
                let respond = new Array;
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
// // \\ // \\ // \\
app.post("/Load/savefile", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const ID = req.body.SaveFile_ID;
        const GUID = req.body.GUID;
        if (GUID != null) {
            if (ID instanceof (Array)) {
            }
            else if (ID instanceof Number) {
                GenerateSaveFile(ID);
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
                let saveFile;
                const sf = yield Database_1.DB.select.SaveFile(GUID);
                const profile = yield Database_1.DB.select.Profile(ID);
                const map = yield Database_1.DB.select.Map(ID);
                const statistics = yield Database_1.DB.select.statistics(ID);
                const cells = yield Database_1.DB.select.Cell(ID);
                let objectinfos;
                for (const cell in cells) {
                    const objInfo = yield Database_1.DB.select.Objinfo(cell.x, cell.y, GUID);
                    objectinfos.push(objInfo[0]);
                }
                //const objInfo = await DB.select.SaveFile(GUID);
            });
        }
    });
});
// \\ // \\ // \\ //
app.post("/CreateUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const asyncHash = yield EncryptPasswordASync(req.body.Password);
        Database_1.DB.insert.User(req.body.GUID, req.body.UserName, asyncHash, function (info) {
            res.send(JSON.stringify(info));
        });
    });
});
app.post("/LoadUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        //needs guid username and password
        let data = { UserName: req.body.UserName, Password: req.body.Password };
        if (req.body.GUID != null) {
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
app.post("/DeleteUser", function (req, res) {
    //removes player with savefiles
    //deletes profile with saved guid
});
function EncryptPasswordASync(password) {
    return __awaiter(this, void 0, void 0, function* () {
        const hash = yield bcrypt.hash(password, 10);
        return hash;
    });
}
function comparePassword(password, hash, callback) {
    return __awaiter(this, void 0, void 0, function* () {
        callback(yield bcrypt.compare(password, hash));
    });
}
//# sourceMappingURL=index.js.map