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

  InsertUser(guid: string, name: string, password : Promise<any>){

    var sql = `INSERT INTO Users (UserId,UserName,password) VALUES (${guid},${name},${password})`;

    this.conn.query(sql, function (err, result) {
      if(err) throw err;

    });
  }

}
export const DB = new Database("localhost", "root", "", "factorygame");

export enum Tables{
  Cell = "x,y,ObjectTypes,Map,Map_SaveFile_Id",
  Map = "xRange,yRange,SaveFile_Id",
  ObjectInfo = "dataStored,powerStored,Level,Age,upkeepCost,dataMined,dataSold,datTransferd,cells_profile_SaveFile_Id",
  Profile = "DateMade,DateSeen,TimePlayed,SaveFile_Id",
  SaveFile = "SaveName",
  Statistics = "Networth,Money,Data,Xp,Level,SaveFile_Id",
}