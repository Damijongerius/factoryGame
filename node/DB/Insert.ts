import { Profile, SaveFile, Statistics, Map, cells, ObjInfo } from "../models/SaveFile";

export class Insert{
    conn: any;
    constructor(conn: any) {
      this.conn = conn;
    }
    SaveFile(sf: SaveFile, GUID: string) {
      const { Name } = sf.profile;
  
      let lastId: number = -1;
  
      var sql = `INSERT INTO savefile (SaveName, users_UserId) VALUES ("${Name}", "${GUID}")`;
  
      return new Promise<number>((resolve, reject) => {
        this.conn.query(sql, (err, result) => {11
          return err ? reject(err) : resolve(result.insertId);
        });
      });
    }
  
    Profile(p: Profile, saveFileID: number) {
      const { DateMade, DateSeen, TimePlayed } = p;
  
      var sql = `INSERT INTO profile (DateMade,DateSeen,TimePlayed,savefile_ID) VALUES ("${DateMade}","${DateSeen}","${TimePlayed}", ${saveFileID})`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    statistics(p: Statistics, saveFileId: number) {
      const { networth, money, data, xp, Level } = p;
  
      var sql = `INSERT INTO statistics (Networth,Money,Data,Xp,Level, savefile_ID) VALUES (${networth},${money},${data},${xp},${Level},${saveFileId})`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Map(p: Map, saveFileId: number) {
      const { xRange, yRange } = p;
  
      var sql = `INSERT INTO map (xRange,yRange,savefile_ID) VALUES (${xRange},${yRange},${saveFileId})`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Cell(p: cells, saveFileId: number) {
      const { x, y, objType } = p;
  
  
      var sql = `INSERT INTO cells ( x, y, ObjectTypes,savefile_ID) VALUES (${x},${y},${objType},${saveFileId})`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Objinfo(p: ObjInfo, x: number, y: number, saveFileId: number) {
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
  
      var sql = `INSERT INTO objectinfo (dataStored, powerStored, level, age, upkeepCost, dataMined, dataSold, dataTransferd, cells_x, cells_y,savefile_ID) VALUES (${dataStored}, ${powerStored}, ${level}, ${age}, ${upkeepCost}, ${dataMined}, ${dataSold}, ${dataTransferd}, ${x}, ${y}, ${saveFileId})`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    User(
      guid: string,
      name: string,
      password: string,
      callback: Function
    ): any {
      var sql = `INSERT INTO users (UserId,UserName,password) VALUES ("${guid}","${name}","${password}")`;
  
      this.conn.query(sql, function (err, result) {
        if (err) {
          console.log("err");
          callback({ status: 0, message: "was not able to create your account" });
        } else {
          console.log("not err");
          callback({ status: 1, message: "your account has been created" });
        }
      });
    }
  }