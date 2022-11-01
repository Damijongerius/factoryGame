import { debug } from "console";
import { stringify } from "querystring";
import internal from "stream";
import { Info, Map, SaveFile, Statistics } from "../models/SaveFile";
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
    this.conn.connect(function (err) {
      if (err) throw err;
      console.log("Connected To DataBase!");
    });
  }

  InsertSaveFile(sf: SaveFile, GUID: string, callBack: Function): number{
    const { Name } = sf.profile;

    let lastId: number = -1

    var sql = `INSERT INTO saveFile (SaveName, users_UserId) VALUES ("${Name}", "${GUID}")`;

    this.conn.query(sql, function (err, result) {
      if (err) throw err;

      lastId = result.insertId;
    });

    return lastId;

    // callBack(0);
  }

  InsertInfo(p: Info, saveFileID: number): number {
    const { DateMade, DateSeen, TimePlayed } = p;

    var sql = `INSERT INTO Info (DateMade,DateSeen,TimePlayed,savefile_ID) VALUES ("${DateMade}","${DateSeen}","${TimePlayed}", ${saveFileID})`;

    this.conn.query(sql, function (err, result) {
      if (err) throw err;

      return result.insertId;
    });

    return 0;
  }

  Insertstatistics(p: Statistics, saveFileId: number){
    const { networth,money,data,xp,Level} = p;

    var sql = `INSERT INTO statistics (Networth,Money,Data,Xp,Level, _savefile_ID) VALUES (${networth},${money},${data},${xp},${Level},${saveFileId})`

    this.conn.query(sql, function (err, result){
      if (err) throw err;
    });
  }

  InsertMap(p: Map, saveFileId: number){
    const {xRange, yRange} = p

    var sql = `INSERT INTO map (xRange,yRange,savefile_ID) VALUES (${xRange},${yRange},${saveFileId})`;

    this.conn.query(sql, function (err, result){
      if (err) throw err;

    });
  }

  Insertobjectinfo(p: Info, saveFileId: number){
    const {dataStored, powerStored, level, age, upkeepCost, dataMined, dataSold, dataTransferd} = p;

    var sql = `INSERT INTO objectinfo (dataStored, powerStored, level, age, upkeepCost, dataMined, dataSold, dataTransferd, map_savefile_ID) VALUES ()`
  }

  InsertUser(
    guid: string,
    name: string,
    password: string,
    callback: Function
  ): any {
    var sql = `INSERT INTO Users (UserId,UserName,password) VALUES ("${guid}","${name}","${password}")`;

    this.conn.query(sql, function (err, result) {
      if (err) {
        callback({ status: 0, message: "was not able to create your account" });
      } else {
        callback({ status: 1, message: "your account has been created" });
      }
    });
  }

  SelectUser(data: any, callback: Function) {
    let sql;
    if (data.GUID == null) {
      sql = `SELECT * FROM Users WHERE UserName = "${data.UserName}" `;
    } else {
      sql = `SELECT * FROM Users WHERE UserId = "${data.GUID}"`;
    }

    this.conn.query(sql, function (err, result) {
      if (err) {
        callback({ status: 3, message: "Was not able to find user" });
        console.log(err);
      } else {
        callback({
          status: 2,
          message: "This is what i found",
          result: result,
        });
      }
    });
  }
}

export const DB = new Database("localhost", "root", "", "factorygame");
