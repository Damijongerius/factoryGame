import * as EH from "./ErrorHandler";
var mysql = require("mysql");

class Database {
  conn: any;
  errorHandler: EH.ErrorHandler;

  constructor(host: string, user: string, password: string, database: string) {
    this.conn = mysql.createConnection({
      host: host,
      user: user,
      password: password,
      database: database,
    });

    this.errorHandler = new EH.ErrorHandler();
    this.conn.connect();
  }

  Checks() {}

  select(table: string) {
    this.conn.query(`SELECT * FROM ${table}`);
  }

}
export const DB = new Database("localhost", "root", "", "Factorygame");
