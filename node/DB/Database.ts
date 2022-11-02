import { debug } from "console";
import { umask } from "process";
import { stringify } from "querystring";
import internal from "stream";
import {
  Map,
  ObjInfo,
  Profile,
  SaveFile,
  Statistics,
} from "../models/SaveFile";
import * as EH from "./ErrorHandler";
import { cells } from "../models/SaveFile";
import { Insert } from './Insert';
var mysql = require("mysql");

export class Database {
  conn: any;
  errorHandler: EH.ErrorHandler;

  insert: Insert;

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

    this.insert = new Insert(this.conn);
  } 

  SelectUser(data: any, callback: Function) {
    let sql;
    if (data.GUID == null) {
      sql = `SELECT * FROM users WHERE UserName = "${data.UserName}" `;
    } else {
      sql = `SELECT * FROM users WHERE UserId = "${data.GUID}"`;
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
