import { app } from "../..";
import { DB } from "../DB/Database";
import { EncryptPasswordASync, comparePassword } from "../Encryption/EncryptionManager";

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
  // \\ // \\ // \\ //
  
  //onUserLoadRequest
  // // \\ // \\ // \\
  app.post("/LoadUser", async function (req, res) {
    //needs guid username and password
    const data: any = {
      UserName: req.body.UserName,
      Password: req.body.Password,
    };
    if (req.body.GUID !== null) {
      data.GUID = req.body.GUID;
    }
    const info: any = await DB.select.User(data);
    let send;
    switch (info.status) {
      case 3: {
        send = {
          Status: 3,
          message: `no existing user with name ${data.UserName}`,
        };
        break;
      }
  
      case 2: {
        try {
          if (info.result.length == 0 || info.result == null) {
            console.log("nothing");
            send = {
              Status: 5,
              message: "incorrect password try again",
            };
            throw send;
          }
          info.result.forEach(async function (i, idx, array) {
            comparePassword(
              req.body.Password,
              array[idx].password,
              function (params: boolean) {
                if (params) {
                  
                  console.log("match");
                  send = {
                    Status: 4,
                    message: `the password matches ${data.UserName}`,
                    Info: {
                      UserName: array[idx].UserName,
                      GUID: array[idx].UserId,
                    }
                    
                  };
                  throw send;
                } else {
                  if (i === array.length - 1) {
                    console.log("not match");
                    send = {
                      Status: 5,
                      message: "incorrect password try again",
                    };
                    throw send;
                  }
                }
              }
            );
          });
          break;
        } catch(e) {
          throw e;
        }
      }
      default: {
        console.log("error");
        res.send({
          Status: 101,
          message: "error on our end",
        });
        break;
      }
    }
  });
  // \\ // \\ // \\ //
  
  //onUserDeleteRequest
  // // \\ // \\ // \\
  app.post("/DeleteUser", async function (req, res) {
    if (req.body.GUID != null) {
      DB.delete.User(req.body.GUID);
    }
  });
  // \\ // \\ // \\ //
  
  //onSaveFileDeleteRequest
  // // \\ // \\ // \\
  app.post("/DeleteSaveFile", async function (req, res) {
    if (req.body.GUID != null && req.body.saveName != null) {
      DB.delete.SaveFile(req.body.GUID, req.body.saveName);
    }
  });
  // \\ // \\ // \\ //