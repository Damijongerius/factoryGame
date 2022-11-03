import { SaveFile, Convert, Map, Statistics, Profile, ObjInfo, cells } from './models/SaveFile';
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

app.post("/load/profiles", async function (req,res){
  if(req.body.GUID  != null){
    const result: any = await DB.select.SaveFile(req.body.GUID);
    if(result != null){
      let respond: Array<Profile> = new Array<Profile>;
      for(const rs of result){
        const profiles: any = await DB.select.Profile(rs.ID);
        respond.push(profiles[0]);
      }
        res.send({profiles: respond, saveFiles: result});
    }
    else{
      res.send({status: 101, message: "unknown ERROR"});
    }
  }
  else{
    res.send({status: 11, message: "need valid GUID"});
  }
});
// // \\ // \\ // \\
app.post("/Load/savefile", async function (req, res){
  const ID = req.body.ID;
  const GUID = req.body.GUID;
  if(GUID != null){
  if(ID instanceof Array<number>){

  }else if(ID instanceof Number){
    GenerateSaveFile(ID);
  }else{
    res.send({status: 13, message: "need valid ID"});
  }
}
else{
  res.send({status: 11, message: "need valid GUID"});
}

async function GenerateSaveFile(ID: Number){
  let saveFile: SaveFile;
  const sf = await DB.select.SaveFile(GUID);
  const profile = await DB.select.Profile(ID);
  const map = await DB.select.Map(ID);
  const statistics = await DB.select.statistics(ID);
  const cells: object[] = await DB.select.Cell(ID);

  let objectinfos;
  if(cells instanceof Array<Object>)
  for(var cell in cells){
    const objInfo: any = await DB.select.Objinfo(cell.x, cell.y, GUID);
    objectinfos.push(objInfo[0]);
  }
  
  //const objInfo = await DB.select.SaveFile(GUID);
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
