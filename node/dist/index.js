"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
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
    console.log(a.Profile.Name);
    console.log(a.Profile.statistics.money);
    //data = JSON.parse(req.body);
    // console.log(a.map.grid[0]);
});
app.post("/recieve", function (req, res) {
    let object = JSON.parse(req.body.sendJson);
    console.log(object.Profile.statistics.money);
    // savefFile.ConstructProfile(
    //   object.profile.Name,
    //   object.profile.DateMade,
    //   object.profile.DateSeen,
    //   object.profile.TimePlayed
    // );
    // savefFile.Profile.ConstructStats(
    //   object.profile.networth,
    //   object.profile.money,
    //   object.profile.data,
    //   object.profile.xp,
    //   object.profile.level
    // );
    // saveFile.ConstructMap(object.Map.xRange, object.Map.yRange);
    // for (let i; i < length; i++) {
    //   saveFile.Map.ConstructCell(
    //     object.Map.cell.x,
    //     object.Map.cell.y,
    //     object.Map.cell.type
    //   );
    //   saveFile.Map.cell[i].building(
    //     object.object.Map.cell.objectInfo.dataStored,
    //     object.object.Map.cell.objectInfo.powerStored,
    //     object.object.Map.cell.objectInfo.level,
    //     object.object.Map.cell.objectInfo.age,
    //     object.object.Map.cell.objectInfo.upkeepCost,
    //     object.object.Map.cell.objectInfo.dataMined,
    //     object.object.Map.cell.objectInfo.dataSold,
    //     object.object.Map.cell.objectInfo.dataTransferd
    //   );
    // }
});
//# sourceMappingURL=index.js.map