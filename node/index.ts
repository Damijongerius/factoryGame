import { profile } from "console";
import { savefFile } from "./models/SaveFile";

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

  //data = JSON.parse(req.body);

  // console.log(a.map.grid[0]);
});

app.post("/recieve", function (req, res) {
  let object = JSON.parse(req.body.sendJson);

  savefFile.ConstructProfile(
    object.profile.Name,
    object.profile.DateMade,
    object.profile.DateSeen,
    object.profile.TimePlayed
  );

  savefFile.Profile.ConstructStats(
    object.profile.networth,
    object.profile.money,
    object.profile.data,
    object.profile.xp,
    object.profile.level
  );
  saveFile.ConstructMap(object.Map.xRange, object.Map.yRange);

  //object.map.cell is een array
  //vraag om de length van de array en maak alles aan
  //het zou kunnen dat ik de constructor daarvoor verkeerd heb geschreven maar ik geloof dat jij het wel kan fixen
  for(let i; i < 10; i++){
    
  }

  //er is niet 1 cell er zijn er meer
  saveFile.Map.ConstructCell(
    object.Map.cell.x,
    object.Map.cell.y,
    object.Map.cell.type,
  );

  //zelfde geld voor deze 
  //en object.buidling bestaat niet
  //het is object.Map.cell.objectInfo
  //en waar zijn de haakjes lol
  saveFile.building(object.building.x);
  object.building.dataStored,
    object.building.powerStored,
    object.building.level,
    object.building.upkeepCost,
    object.building.dataMined,
    object.building.dataSold,
    object.building.dataTransferd;
    
});
