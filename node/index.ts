import { SaveFile, Convert, Map, Statistics } from "./models/SaveFile";
import { DB } from "./DB/Database";
import { isNativeError } from "util/types";
import { stringify } from 'querystring';

const { saveFile } = require("./models/SaveFile");
//node module express
const { response, request, json } = require("express");

const express = require("express");

const bcrypt = require("bcrypt");

//node module body parser
const bodyParser = require("body-parser");

const app = express();

app.use(express.json());

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.listen(3000, function () {
  console.log("server running");
});

// // \\ // \\ // \\
app.post("/Save/savefile", async function (req, res) {
  const saveFile: SaveFile = Convert.toSaveFile(req.body.sendJson);
  console.log(saveFile);
  const { map, profile } = saveFile;
  const result = await DB.insert.SaveFile(saveFile, req.body.GUID);
  console.log(result);
  DB.insert.Profile(profile, result);
  DB.insert.statistics(profile.Statistics, result);
  DB.insert.Map(map, result);
  map.grid.forEach((element, index, array) => {
    DB.insert.Cell(element, result);
    DB.insert.Objinfo(element.ObjInfo, element.x, element.y, result);
  });
});
// \\ // \\ // \\ //

// // \\ // \\ // \\
app.post("/Load/savefile", function (req, res){
  req.body.LastSeen;
  req.body.lastSeen[0];

  req.body.SaveName
  if(req.body.GUID !=null){
    if(req.body.SaveName instanceof Array){
      //get all savefiles with no lastseen time
      //or outdated
    }
    else if(req.body.SaveName != null){
      //find 1 savefile
    }
    else{
     res.send({status: 10, message: "there was not enough information need LastSeen"});
     }
    }
    else{
      res.send({status: 11, message: "need valid GUID"});
    }
  });
// \\ // \\ // \\ //

app.post("/CreateUser", async function (req, res) {
  const asyncHash = await EncryptPasswordASync(req.body.Password);
  DB.insert.User(
    req.body.GUID,
    req.body.UserName,
    asyncHash,
    function (info: object) {
      res.send(JSON.stringify(info));
    }
  );
});

app.post("/LoadUser", async function (req, res) {
  //needs guid username and password
  let data: any = { UserName: req.body.UserName, Password: req.body.Password };
  if (req.body.GUID != null) {
    data.GUID = req.body.GUID;
  }
  DB.select.User(data, async function (info: any) {
    switch (info.status) {
      case 3: {
        res.send({
          Status: 3,
          message: `no existing user with name ${data.UserName}`,
        });
        break;
      }

      case 2: {
        info.result.forEach(async function (i, idx, array) {
          comparePassword(
            req.body.Password,
            array[idx].password,
            function (params: boolean) {
              if (params) {
                res.send({
                  Status: 4,
                  message: `the password matches ${data.UserName}`,
                  Info: {
                    UserName: array[idx].UserName,
                    GUID: array[idx].UserId,
                  },
                });
              } else {
                if (i === array.length - 1) {
                  res.send({
                    Status: 5,
                    message: "incorrect password try again",
                  });
                }
              }
            }
          );
        });
        break;
      }
      default: {
        res.send({
          Status: 101,
          message: "error on our end",
        });
        break;
      }
    }
  });
});

app.post("/DeleteUser", function (req, res) {
  //removes player with savefiles
  //deletes profile with saved guid
});

async function EncryptPasswordASync(password) {
  const hash = await bcrypt.hash(password, 10);
  return hash;
}

async function comparePassword(password, hash, callback: Function) {
  callback(await bcrypt.compare(password, hash));
}
