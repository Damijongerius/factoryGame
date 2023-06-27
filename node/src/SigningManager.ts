import { Request, Response } from "express";
import { SqlManager } from "./Queries/SqlManager";
import { EncryptPasswordASync } from "./encryptor";

const sqlManager : SqlManager = new SqlManager();

export class SignManager{

    async CreateUser(req: Request, res: Response){
        sqlManager.insertUser(req.body.GUID,req.body.UserName,await EncryptPasswordASync(req.body.Password));

        res.send({status: 1, message : "User created successfully"});
    }

    DeleteUser(req: Request, res: Response){
        console.log("delete" + req.body.GUID);
    }

    GetUser(req: Request, res: Response){
        console.log("get" + req.body.GUID + "-" + req.body.UserName + "-" + req.body.Password);
    }
}