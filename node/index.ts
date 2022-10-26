import { profile } from "console";
import { SaveFile, Convert, Map, Info } from './models/SaveFile';
import {DB} from "./DB/Database";
import { Encoding, Hash, LargeNumberLike } from "crypto";
import { stringify } from 'querystring';


const { saveFile } = require("./models/SaveFile");
//node module express
const { response, request, json } = require("express");

const express = require("express");

const bcrypt = require ('bcrypt');

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
  const saveFile : SaveFile = Convert.toSaveFile(req.body.sendJson);

  const {map, Info } = saveFile;
   var lastID = DB.InsertSaveFile(saveFile);
});
// \\ // \\ // \\ //

app.post("/GetSF", function (req,res){
  
});


app.post("/CreateUser", async function (req, res){
const asyncHash = await EncryptPasswordASync(req.body.Password) 
  const err = DB.InsertUser(req.body.GUID,req.body.UserName,asyncHash);
  if (err) {
    res.send(JSON.stringify({"status": 0, "Message" : "was not able to make profile"}))
  }
  res.send(JSON.stringify({"status": 1, "Message": "account sucsessfully created"}));
})

app.post("/LoadUser", function (req, res){
//needs guid and password
})

app.post("/DeleteUser", function (req, res){
//removes player with savefiles
})


async function EncryptPasswordASync(password) {
  const hash = await bcrypt.hash(password, 10);
  return hash;
}


async function comparePassword(password, hash) {
  const result = await bcrypt.compare(password, hash);
  return result;
}
