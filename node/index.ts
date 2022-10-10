import { debug, profile } from "console";
import { SaveFile, Convert } from "./models/SaveFile";

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
  let a = Convert.toSaveFile(req.body.sendJson);

  console.log(a.profile.Name);

  console.log(a.profile.Statistics.money);

  //data = JSON.parse(req.body);

  // console.log(a.map.grid[0]);
});

app.post("/recieve", function (req, res) {
  const saveFile: SaveFile = Convert.toSaveFile(req.body.sendJson);
  console.log("1");
  DB.InsertInto("savefile", Tables.SaveFile, `'${saveFile.profile.Name}'`);

  console.log("2");
  //for (let i = 0; i < saveFile.map.grid.length; i++) {
  //  DB.InsertInto("Cell", Tables.Cell, `'${saveFile.map.grid[i]}'`);
  //}

  console.log("3");
  //DB.InsertInto(
  //  "Map",
  //  Tables.Map,
  //  `'${saveFile.map.xRange}','${saveFile.map.yRange}'`
  //);

  console.log("4");
  //for (let i = 0; i < saveFile.map.grid.length; i++) {
  //  DB.InsertInto(
  //    "ObjectInfo",
  ////    Tables.ObjectInfo,
  //  `'${saveFile.map.grid[i].info}'`
  //   );
  //  }
  console.log("5");
  //DB.InsertInto("Profile", Tables.Profile, `'${saveFile.profile}'`);
  console.log("6");
  // DB.InsertInto("SaveFile", Tables.SaveFile, `'${saveFile}'`);
  console.log("7");
  // DB.InsertInto(
  //  "Statistics",
  //  Tables.Statistics,
  //  `'${saveFile.profile.Statistics}'`
  // );
});

app.post("/GetSF", function (req, res) {});
