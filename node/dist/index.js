"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const SaveFile_1 = require("./models/SaveFile");
const { DB, Tables } = require("./DB/Database");
const { saveFile } = require("./models/SaveFile");
//node module express
const { response, request, json } = require("express");
const express = require("express");
const app = express();
app.use(express.json());
//node module body parser
const bodyParser = require("body-parser");
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.get("/ping", (req, res) => {
    res.send("pong");
});
app.listen(3000, function () {
    console.log("server draai");
});
app.post("/senddata", function (req, res) {
    console.log(req.body);
    let a = SaveFile_1.Convert.toSaveFile(req.body.sendJson);
    console.log(a.profile.Name);
    console.log(a.profile.Statistics.money);
    //data = JSON.parse(req.body);
    // console.log(a.map.grid[0]);
});
app.post("/recieve", function (req, res) {
    //let o: SaveFile = JSON.parse(req.body.sendJson);
    //console.log(o.profile.Statistics.money);
    //console.log(o.profile.Name);
    const saveFile = SaveFile_1.Convert.toSaveFile(req.body.sendJson);
    DB.InsertInto("saveFile", Tables.SaveFile, saveFile.profile.Name);
});
app.post("/GetSF", function (req, res) {
});
//# sourceMappingURL=index.js.map