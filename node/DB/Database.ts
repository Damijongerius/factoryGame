import { debug } from "console";
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

    const returnData = this.insertUser(sql)
    returnData.then((RtD) => {  
      return RtD;
    })

  }

    async insertUser(sql : string): Promise<object>{
      this.conn.query(sql, function (err, result){
        if(err) {
          console.error(err);
          return {"errorCode" : 2, "message" : err};
        }
        if(!err){
          return {"errorCode" : 1, "message" : "a new account has been created"};
        }
  
      })
       return {"errorCode" : 1, "message" : "a new account has been created"};
    }

  }

export const DB = new Database("localhost", "root", "", "factorygame");