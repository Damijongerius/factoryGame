import internal from "stream";
import * as EH from "./ErrorHandler";
var mysql = require("mysql");

class Database {
  conn: any;
  errorHandler: EH.ErrorHandler;

  private LastInsertedId: BigInt;

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

  InsertInto(TableName : string, table : Tables, values : any){
    var sql = `INSERT INTO ${TableName} (${table}) VALUES (${values})`;

    this.conn.query(sql, function (err, result) {
      if(err) throw err;

      console.log(result.insertId);
    })
  }

  GetLastInsterted(): any{
    return this.LastInsertedId;
  }

}
export const DB = new Database("localhost", "root", "", "factorygame");

export enum Tables{
  Cell = "x,y,ObjectTypes,Map",
  Map = "xRange,yRange,SaveFile_Id",
  ObjectInfo = "dataStored,powerStored,Level,Age,upkeepCost,dataMined,dataSold,datTransferd",
  Profile = "DateMade,DateSeen,TimePlayed",
  SaveFile = "SaveName",
  Statistics = "Networth,Money,Data,Xp,Level",
}