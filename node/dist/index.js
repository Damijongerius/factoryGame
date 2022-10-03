"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const SaveFile_1 = require("./models/SaveFile");
const { DB } = require("./DB/Database");
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
    let a = JSON.parse(req.body.sendJson);
    console.log(a.profile.Statistics.money);
});
app.post("/recieve", function (req, res) {
    let object = JSON.parse(req.body.sendJson);
    SaveFile_1.savefFile.ConstructProfile(object.profile.Name, object.profile.DateMade, object.profile.DateSeen, object.profile.TimePlayed);
    SaveFile_1.savefFile.Profile.ConstructStats(object.profile.networth, object.profile.money, object.profile.data, object.profile.xp, object.profile.level);
    saveFile.ConstructMap(object.Map.xRange, object.Map.yRange);
    for (let i = 0; i < length; i++) {
        saveFile.Map.ConstructCell(object.Map.cell.x, object.Map.cell.y, object.Map.cell.type);
        saveFile.Map.cell[i].building(object.Map.cell[i].objectInfo.dataStored, object.Map.cell[i].objectInfo.powerStored, object.Map.cell[i].objectInfo.level, object.Map.cell[i].objectInfo.age, object.Map.cell[i].objectInfo.upkeepCost, object.Map.cell[i].objectInfo.dataMined, object.Map.cell[i].objectInfo.dataSold, object.Map.cell[i].objectInfo.dataTransferd);
    }
});
//# sourceMappingURL=index.js.map