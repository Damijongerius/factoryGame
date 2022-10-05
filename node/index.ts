import { profile } from "console";
import { SaveFile, Convert } from "./models/SaveFile";

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
  let a = Convert.toSaveFile(req.body.sendJson);

  console.log(a.profile.Name);

  console.log(a.profile.Statistics.money);

  //data = JSON.parse(req.body);

  // console.log(a.map.grid[0]);
});

app.post("/recieve", function (req, res) {
  let o: SaveFile = JSON.parse(req.body.sendJson);
  console.log(o.profile.Statistics.money);
  console.log(o.profile.Name);
  res.body(Convert.saveFileToJson(o));
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
