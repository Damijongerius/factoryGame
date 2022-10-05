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
  res.send(Convert.saveFileToJson(o));

});

app.post("/GetSF", function (req,res){

});
