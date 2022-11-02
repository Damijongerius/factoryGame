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
const Insert_1 = require("./Insert");
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
        this.insert = new Insert_1.Insert(this.conn);
    }
    SelectUser(data, callback) {
        let sql;
        if (data.GUID == null) {
            sql = `SELECT * FROM users WHERE UserName = "${data.UserName}" `;
        }
        else {
            sql = `SELECT * FROM users WHERE UserId = "${data.GUID}"`;
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