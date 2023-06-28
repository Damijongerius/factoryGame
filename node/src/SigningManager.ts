import { Request, Response } from "express";
import { SqlManager } from "./Queries/SqlManager";
import { EncryptPasswordASync, comparePassword } from "./encryptor";

const sqlManager : SqlManager = new SqlManager();

export class SignManager{

    async CreateUser(req: Request, res: Response){
        sqlManager.insertUser(req.body.GUID,req.body.UserName,await EncryptPasswordASync(req.body.Password));

        res.send({status: 1, message : "User created successfully"});
    }

    DeleteUser(req: Request, res: Response){
        sqlManager.deleteUser(req.body.GUID);

        res.send({status: 1, message : "User deleted successfully"});
    }

    async GetUser(req: Request, res: Response){
        console.log(req.body.UserName + "-" + req.body.Password);
        const users : any[] = await sqlManager.getUserWithName(req.body.UserName);
        
        let userS;
        for(let user of users){
            const isTrue = await comparePassword(req.body.Password, user.password);
            if(isTrue){
                res.send({status: 4, message : "User sucessfully logged in", info: {username:  user.UserName, GUID: user.id}})
                userS = user;
            }
            break;
        }

        try {
            res.send({status: 5, message : "incorrect"});
        } catch (error) {
            
        }
    }
}