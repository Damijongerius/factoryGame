import { SaveFile, Convert, Map, Info, Statistics } from "./models/SaveFile";
import { DB } from "./DB/Database";;

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
  console.log("server draai");
});

// // \\ // \\ // \\
app.post("/recieve", function (req, res) {
  const saveFile: SaveFile = Convert.toSaveFile(req.body.sendJson);
  const { map, Info } = saveFile;
  var lastID = DB.InsertSaveFile(saveFile, req.body.GUID, function (insertId: number) {
    DB.InsertInfo(Info, insertId); 
      
    DB.InstertStatistics
  });
});
// \\ // \\ // \\ //

app.post("/GetSF", function (req, res) {});

app.post("/CreateUser", async function (req, res) {
  const asyncHash = await EncryptPasswordASync(req.body.Password);
  DB.InsertUser(
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
  DB.SelectUser(data, async function (info: any) {
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
                    UserName:  array[idx].UserName,
                    GUID:  array[idx].UserId
                  }
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
            message: "error on our end" 
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
