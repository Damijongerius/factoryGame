import internal from "stream";
import { Profile, SaveFile } from "../models/SaveFile";
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

  InsertProfile(p: Profile, saveFileID: number): number{
    var sql = `INSERT INTO saveFile (SaveName) VALUES (${name})`;

    this.conn.query(sql, function (err, result) {
      if(err) throw err;

      return result.insertId;
    });
    
    return 0;
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