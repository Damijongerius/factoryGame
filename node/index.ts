import { profile } from "console";
import { SaveFile, Convert, Map, Info } from './models/SaveFile';
import {DB, Tables} from "./DB/Database";
import { Encoding, Hash, LargeNumberLike } from "crypto";


const { saveFile } = require("./models/SaveFile");
//node module express
const { response, request, json } = require("express");

const express = require("express");

const app = express();

app.use(express.json());

//node module body parser
const bodyParser = require("body-parser");

const saltRounds = 10;

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

  console.log(a.Info.Statistics.money);

  //data = JSON.parse(req.body);

  // console.log(a.map.grid[0]);
});

app.post("/recieve", function (req, res) {
  const saveFile : SaveFile = Convert.toSaveFile(req.body.sendJson);

  const {map, Info } = saveFile;
   var lastID = DB.InsertSaveFile(saveFile);
});

app.post("/GetSF", function (req,res){
  
});

// <=> <=> <=> <=>

app.post("/CreateUser", function (req, res){
//adding a user to database
console.log(req.body)
const asyncHash = EncryptPasswordASync(req.body.Password)
asyncHash.then((h) => {
  DB.InsertUser(req.body.GUID,req.body.UserName,h);
  res.send({h});
})
})

app.post("/LoadUser", function (req, res){
//needs uuid and password
})

app.post("/DeleteUser", function (req, res){
//removes player with savefiles
})

const bcrypt = require ('bcrypt');

async function EncryptPasswordASync(password) {
  const hash = await bcrypt.hash(password, 10);
  return hash
}