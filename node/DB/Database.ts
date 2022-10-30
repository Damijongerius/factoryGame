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
    this.conn.connect(function (err) {
      if (err) throw err;
      console.log("Connected To DataBase!");
    });
  }

  InsertSaveFile(sf: SaveFile): number {
    const { name } = sf;

    var sql = `INSERT INTO saveFile (SaveName) VALUES (${name})`;

    this.conn.query(sql, function (err, result) {
      if (err) throw err;

      return result.insertId;
    });

    return 0;
  }

  InsertInfo(p: Info, saveFileID: number): number {
    const { DateMade, DateSeen, TimePlayed } = p;

    var sql = `INSERT INTO Info () VALUES (${DateMade},${DateSeen},${TimePlayed})`;

    this.conn.query(sql, function (err, result) {
      if (err) throw err;

      return result.insertId;
    });

    return 0;
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
