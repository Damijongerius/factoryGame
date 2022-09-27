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
exports.DB = void 0;
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
        this.conn.connect();
    }
    Checks() { }
    select(table) {
        this.conn.query(`SELECT * FROM ${table}`);
    }
    insert(table, param, obj) {
        let sql = `INSTERT INTO ${table} (${this.ParamToString(param)})`;
        this.conn.query(sql, obj, this.errorHandler.OnInsert);
    }
    ParamToString(param) {
        let string;
        for (let i = 0; i < param.length; i++) {
            if (param.length != i) {
                string += `${param[i]}, `;
            }
            else {
                string += `${param[i]} `;
            }
        }
        return string;
    }
}
exports.DB = new Database("localhost", "root", "", "Factorygame");
//# sourceMappingURL=Database.js.map