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
exports.Tables = exports.DB = exports.Database = void 0;
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
    InsertSaveFile(sf) {
        const { name } = sf;
        var sql = `INSERT INTO saveFile (SaveName) VALUES (${name})`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
            return result.insertId;
        });
        return 0;
    }
    InsertInfo(p, saveFileID) {
        const { DateMade, DateSeen, TimePlayed, } = p;
        var sql = `INSERT INTO Info () VALUES (${DateMade},${DateSeen},${TimePlayed})`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
            return result.insertId;
        });
        return 0;
    }
    InsertUser(guid, name, password) {
        var sql = `INSERT INTO Users (UserId,UserName,password) VALUES (${guid},${name},${password})`;
        this.conn.query(sql, function (err, result) {
            if (err)
                throw err;
        });
    }
}
exports.Database = Database;
exports.DB = new Database("localhost", "root", "", "factorygame");
var Tables;
(function (Tables) {
    Tables["Cell"] = "x,y,ObjectTypes,Map,Map_SaveFile_Id";
    Tables["Map"] = "xRange,yRange,SaveFile_Id";
    Tables["ObjectInfo"] = "dataStored,powerStored,Level,Age,upkeepCost,dataMined,dataSold,datTransferd,cells_profile_SaveFile_Id";
    Tables["Profile"] = "DateMade,DateSeen,TimePlayed,SaveFile_Id";
    Tables["SaveFile"] = "SaveName";
    Tables["Statistics"] = "Networth,Money,Data,Xp,Level,SaveFile_Id";
})(Tables = exports.Tables || (exports.Tables = {}));
//# sourceMappingURL=Database.js.map