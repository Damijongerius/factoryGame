import { Profile, SaveFile, Statistics, Map, cells, ObjInfo } from "../models/SaveFile";

export class Update{
    conn: any;
    constructor(conn: any) {
      this.conn = conn;
    }
  
    Profile(p: Profile, saveFileID: number) {
      const { DateMade, DateSeen, TimePlayed } = p;
  
      var sql = `UPDATE profile SET DateMade = "${DateMade}",DateSeen = "${DateSeen}",TimePlayed = "${TimePlayed}" WHERE savefile_ID = ${saveFileID}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    statistics(p: Statistics, saveFileId: number) {
      const { networth, money, data, xp, Level } = p;
  
      var sql = `UPDATE statistics SET Networth = ${networth}, Money = ${money}, Data = ${data}, Xp = ${xp},Level = ${Level} WHERE _savefile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Cell(p: cells, saveFileId: number) {
      const { x, y, objType } = p;
  
  
      var sql = `UPDATE cells SET x = ${x}, y = ${y}, ObjectTypes = ${objType} WHERE map_savefile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Objinfo(p: ObjInfo, x: number, y: number) {
      const {
        dataStored,
        powerStored,
        level,
        age,
        upkeepCost,
        dataMined,
        dataSold,
        dataTransferd,
      } = p;
  
      var sql = `UPDATE objectinfo SET dataStored = ${dataStored}, powerStored = ${powerStored}, level = ${level}, age  = ${age}, upkeepCost = ${upkeepCost}, dataMined = ${dataMined}, dataSold = ${dataSold}, dataTransferd ${dataTransferd} WHERE cells_x = ${x} AND cells_y = ${y}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  }