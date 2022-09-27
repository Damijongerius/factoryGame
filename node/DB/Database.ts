import * as EH from './ErrorHandler';
var mysql = require("mysql");

export class Database {
  conn: any;
  errorHandler: EH.ErrorHandler;

  setConnection(host: string, user: string, password: string, database: string) {
    this.conn = mysql.createConnection({
      host: host,
      user: user,
      password: password,
      database: database
    });

    setInterval(this.Update(), 100);

    this.errorHandler = new EH.ErrorHandler;
    this.conn.connect();
  }

  Update(): any{
    
  }

  Checks(){

  }

  select(table: string) {
    this.conn.query(`SELECT * FROM ${table}`);
  }

  insert(table: string, param: String[], obj: String[]) {
    let sql = `INSTERT INTO ${table} (${this.ParamToString(param)})`;
    this.conn.query(sql, [obj], this.errorHandler.OnInsert);
  }

  ParamToString(param: String[]): string{
    let string;
    for(let i = 0; i < param.length ; i++){
      if(param.length != i){
        string += `${param[i]}, `;
      }
      else
      {
        string += `${param[i]} `;
      }
    }
    return string;
  }


}

