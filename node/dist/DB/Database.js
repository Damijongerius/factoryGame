"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.DB = exports.Database = void 0;
const EH = __importStar(require("./ErrorHandler"));
var mysql = require("mysql");
class Database {
    constructor(host, user, password, database) {
        this.conn = mysql.createConnection({
            host: host,
            user: user,
            password: password,
            database: database,
        });
        this.errorHandler = new EH.ErrorHandler();
        this.conn.connect(function (err) {
            if (err)
                throw err;
            console.log("Connected To DataBase!");
        });
    }
    InsertSaveFile(sf, GUID, callBack) {
        const { Name } = sf.profile;
        let lastId = -1;
        var sql = `INSERT INTO saveFile (SaveName, users_UserId) VALUES ("${Name}", "${GUID}")`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
            lastId = result.insertId;
        });
        return lastId;
        // callBack(0);
    }
    InsertInfo(p, saveFileID) {
        const { DateMade, DateSeen, TimePlayed } = p;
        var sql = `INSERT INTO Info (DateMade,DateSeen,TimePlayed,savefile_ID) VALUES ("${DateMade}","${DateSeen}","${TimePlayed}", ${saveFileID})`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
            return result.insertId;
        });
        return 0;
    }
    Insertstatistics(p, saveFileId) {
        const { networth, money, data, xp, Level } = p;
        var sql = `INSERT INTO statistics (Networth,Money,Data,Xp,Level, _savefile_ID) VALUES (${networth},${money},${data},${xp},${Level},${saveFileId})`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
        });
    }
    InsertMap(p, saveFileId) {
        const { xRange, yRange } = p;
        var sql = `INSERT INTO map (xRange,yRange,savefile_ID) VALUES (${xRange},${yRange},${saveFileId})`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
        });
    }
    Insertobjectinfo(p, saveFileId) {
        const { dataStored, powerStored, level, age, upkeepCost, dataMined, dataSold, dataTransferd } = p;
        var sql = `INSERT INTO objectinfo (dataStored, powerStored, level, age, upkeepCost, dataMined, dataSold, dataTransferd, map_savefile_ID) VALUES ()`;
    }
    InsertUser(guid, name, password, callback) {
        var sql = `INSERT INTO Users (UserId,UserName,password) VALUES ("${guid}","${name}","${password}")`;
        this.conn.query(sql, function (err, result) {
            if (err) {
                callback({ status: 0, message: "was not able to create your account" });
            }
            else {
                callback({ status: 1, message: "your account has been created" });
            }
        });
    }
    SelectUser(data, callback) {
        let sql;
        if (data.GUID == null) {
            sql = `SELECT * FROM Users WHERE UserName = "${data.UserName}" `;
        }
        else {
            sql = `SELECT * FROM Users WHERE UserId = "${data.GUID}"`;
        }
        this.conn.query(sql, function (err, result) {
            if (err) {
                callback({ status: 3, message: "Was not able to find user" });
                console.log(err);
            }
            else {
                callback({
                    status: 2,
                    message: "This is what i found",
                    result: result,
                });
            }
        });
    }
}
exports.Database = Database;
exports.DB = new Database("localhost", "root", "", "factorygame");
//# sourceMappingURL=Database.js.map