import { Profile, SaveFile, Statistics, Map, cells, ObjInfo } from '../models/SaveFile';

export class Select{
    conn: any;
    constructor(conn: any) {
      this.conn = conn;
    }

    SaveFile(Info: any) {
        if(Info.UserID != null){
            
        }else if(Info.saveFileID != null){

        }
    
        var sql = `SELECT * FROM savefile WHERE SaveName = ${} AND users_UserId = ${}`;
    
        this.conn.query(sql, function (err, result) {
            if (err) throw err;
          });
      }
  
    Profile(saveFileID: number) {
      var sql = `SELECT * FROM profile WHERE savefile_ID = ${saveFileID}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    statistics(p: Statistics, saveFileId: number) {
      const { networth, money, data, xp, Level } = p;
  
      var sql = `SELECT * FROM statistics WHERE _savefile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Map(saveFileId: number) {  
      var sql = `SELECT * FROM map WHERE savefile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Cell(saveFileId: number) {
      var sql = `SELECT * FROM cells WHERE map_savefile_ID = ${saveFileId}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    Objinfo(x: number, y: number) { 
      var sql = `SELECT * FROM objectinfo WHERE cells_x = ${x} AND cells_y = ${y}`;
  
      this.conn.query(sql, function (err, result) {
        if (err) throw err;
      });
    }
  
    User(data: any, callback: Function) {
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