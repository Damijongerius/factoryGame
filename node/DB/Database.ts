import { stringify } from "querystring";
import internal from "stream";
import { Info, SaveFile } from "../models/SaveFile";
import * as EH from "./ErrorHandler";
var mysql = require("mysql");

export class Database {
  conn: any;
  errorHandler: EH.ErrorHandler;

  private LastInsertedId: number;

  constructor(host: string, user: string, password: string, database: string) {
    this.conn = mysql.createConnection({
      host: host,
      user: user,
      password: password,
      database: database,
    });

    this.errorHandler = new EH.ErrorHandler();
    this.conn.connect(function(err) {
      if (err) throw err;
      console.log("Connected To DataBase!");
    });
  }

  InsertSaveFile(sf: SaveFile): number {
    const { name } = sf;

    var sql = `INSERT INTO saveFile (SaveName) VALUES (${name})`;

    this.conn.query(sql, function (err, result) {
      if(err) throw err;

      return result.insertId;
    });

    return 0;
  }

  InsertInfo(p: Info, saveFileID: number): number{

    const { DateMade, DateSeen, TimePlayed, } = p;

    var sql = `INSERT INTO Info () VALUES (${DateMade},${DateSeen},${TimePlayed})`;

    this.conn.query(sql, function (err, result) {
      if(err) throw err;

      return result.insertId;
    });
    
    return 0;
  }

  InsertUser(guid: string, name: string, password: Promise<any>): object{

    var sql = `INSERT INTO Users (UserId,UserName,password) VALUES ("${guid}","${name}","${password}")`;

    let returner : object = this.conn.query(sql, function (err, result): object {
      if(err) {
        console.error(err);
        return {"errorCode" : 2, "message" : err};
      };

      return {"errorCode" : 1, "message" : "a new account has been created"};
    });

    if(returner != null){
      console.log(returner);
      //return returner;
    }
    return {"errorCode" : 0, "message" : "nothing seemed to happen"};
  }
}

export const DB = new Database("localhost", "root", "", "factorygame");