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
const Database_1 = require("./DB/Database");
;
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
    console.log("server draai");
});
// // \\ // \\ // \\
app.post("/recieve", function (req, res) {
    //const saveFile: SaveFile = Convert.toSaveFile(req.body.sendJson);
    //const { map, Info } = saveFile;
    //var lastID = DB.InsertSaveFile(saveFile);
});
// \\ // \\ // \\ //
app.post("/GetSF", function (req, res) { });
app.post("/CreateUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const asyncHash = yield EncryptPasswordASync(req.body.Password);
        Database_1.DB.InsertUser(req.body.GUID, req.body.UserName, asyncHash, function (info) {
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
        Database_1.DB.SelectUser(data, function (info) {
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
                                                GUID: array[idx].UserId
                                            }
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
                            message: "error on our end"
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