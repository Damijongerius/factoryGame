import { rejects } from 'assert';
import { Profile, SaveFile, Statistics, Map, cells, ObjInfo } from '../models/SaveFile';

export class Select{
    conn: any;
    constructor(conn: any) {
      this.conn = conn;
    }

    SaveFile(GUID: string){
        var sql = `SELECT * FROM savefile WHERE users_UserId = ${GUID}`;
    
        return new Promise<unknown>((resolve, reject) => {
          this.conn.query(sql, (err, result) => {
            return err ? reject(err) : resolve(result);
          });
        });
      }
  
    Profile(saveFileID: number) {
      var sql = `SELECT * FROM profile WHERE savefile_ID = ${saveFileID}`;
  
      return new Promise<unknown>((resolve, reject) => {
        this.conn.query(sql, (err, result) => {
          return err ? reject(err) : resolve(result);
        });
      });
    }
  
    statistics(saveFileId: number) {
  
      var sql = `SELECT * FROM statistics WHERE savefile_ID = ${saveFileId}`;
  
      return new Promise<unknown>((resolve, reject) => {
        this.conn.query(sql, (err, result) => {
          return err ? reject(err) : resolve(result);
        });
      });
    }
  
    Map(saveFileId: number) {  
      var sql = `SELECT * FROM map WHERE savefile_ID = ${saveFileId}`;
  
      return new Promise<unknown>((resolve, reject) => {
        this.conn.query(sql, (err, result) => {
          return err ? reject(err) : resolve(result);
        });
      });
    }
  
    Cell(saveFileId: number) {
      var sql = `SELECT * FROM cells WHERE savefile_ID = ${saveFileId}`;
  
      return new Promise<unknown>((resolve, reject) => {
        this.conn.query(sql, (err, result) => {
          return err ? reject(err) : resolve(result);
        });
      });
    }
  
    Objinfo(x: number, y: number) { 
      var sql = `SELECT * FROM objectinfo WHERE cells_x = ${x} AND cells_y = ${y}`;
  
      return new Promise<unknown>((resolve, reject) => {
        this.conn.query(sql, (err, result) => {
          return err ? reject(err) : resolve(result);
        });
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