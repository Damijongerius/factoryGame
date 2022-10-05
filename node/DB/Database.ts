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

  InsertInto(){
    
  }

}
export const DB = new Database("localhost", "root", "", "Factorygame");
