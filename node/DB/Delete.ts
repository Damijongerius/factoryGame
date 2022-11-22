import { Profile, SaveFile, Statistics, Map, cells, ObjInfo } from "../models/SaveFile";

export class Delete{
    conn: any;
    constructor(conn: any) {
      this.conn = conn;
    }
    SaveFile(GUID: string, saveName: string) {
      var sql = `DELETE FROM savefile WHERE users_UserId = "${GUID}" AND SaveName = "${saveName}"`;
  
      
        this.conn.query(sql, (err, result) => {
          if(err) throw err;
        });
    }
  
    Profile(saveFileId: number) {
  
      var sql = `DELETE FROM cells WHERE saveFile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    statistics(saveFileId: number) {
  
      var sql = `DELETE FROM statistics WHERE saveFile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Map(saveFileId: number) {
  
      var sql = `DELETE FROM map WHERE saveFile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Cell(saveFileId: number) {
  
      var sql = `DELETE FROM cells WHERE saveFile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Objinfo(saveFileId: number) {

  
      var sql = `DELETE FROM objectinfo WHERE savefile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    User(guid: string,): any {
      var sql = `DELETE FROM users WHERE userId = "${guid}"`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  }