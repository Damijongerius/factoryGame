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
const bcrypt = require('bcrypt');
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
    const saveFile = SaveFile_1.Convert.toSaveFile(req.body.sendJson);
    const { map, Info } = saveFile;
    var lastID = Database_1.DB.InsertSaveFile(saveFile);
});
// \\ // \\ // \\ //
app.post("/GetSF", function (req, res) {
});
app.post("/CreateUser", function (req, res) {
    return __awaiter(this, void 0, void 0, function* () {
        const asyncHash = yield EncryptPasswordASync(req.body.Password);
        const err = Database_1.DB.InsertUser(req.body.GUID, req.body.UserName, asyncHash);
        if (err) {
            res.send(JSON.stringify({ "status": 0, "Message": "was not able to make profile" }));
        }
        res.send(JSON.stringify({ "status": 1, "Message": "account sucsessfully created" }));
    });
});
app.post("/LoadUser", function (req, res) {
    //needs guid and password
});
app.post("/DeleteUser", function (req, res) {
    //removes player with savefiles
});
function EncryptPasswordASync(password) {
    return __awaiter(this, void 0, void 0, function* () {
        const hash = yield bcrypt.hash(password, 10);
        return hash;
    });
}
function comparePassword(password, hash) {
    return __awaiter(this, void 0, void 0, function* () {
        const result = yield bcrypt.compare(password, hash);
        return result;
    });
}
//# sourceMappingURL=index.js.map