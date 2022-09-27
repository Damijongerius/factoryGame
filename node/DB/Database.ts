import { ObjectEncodingOptions } from 'fs';
import * as ErrorHandler from './ErrorHandler';
var mysql = require("mysql");

export class Database {
  conn: any;

  setConnection(host: string, user: string, password: string, database: string) {
    this.conn = mysql.createConnection({
      host: host,
      user: user,
      password: password,
      database: database
    });

    setInterval(this.Update(), 100);
    this.conn.connect()
  }

  Update(): any{
    
  }

  Checks(){

  }

  select() {
    this.conn.query();
  }

  insert(table: string, param: String[], obj: object) {
    this.conn.query(`INSTERT INTO ${table} (${param})`);
  }

  ParamToString(param: String[]){
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
  }


}

