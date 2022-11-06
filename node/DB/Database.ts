import { debug } from "console";
import { umask } from "process";
import { stringify } from "querystring";
import internal from "stream";
import {
    Map,
    ObjInfo,
    Profile,
    SaveFile,
    Statistics,
} from "../models/SaveFile";
import * as EH from "./ErrorHandler";
import { cells } from "../models/SaveFile";
import { Insert } from "./Insert";
import { Update } from "./Update";
import { Select } from "./Select";
const mysql = require("mysql");

export class Database {
    conn: any;
    errorHandler: EH.ErrorHandler;

    insert: Insert;
    update: Update;
    select: Select;

    constructor(
        host: string,
        user: string,
        password: string,
        database: string
    ) {
        this.conn = mysql.createConnection({
            host: host,
            user: user,
            password: password,
            database: database,
        });

        this.errorHandler = new EH.ErrorHandler();
        this.conn.connect(function (err) {
            if (err) throw err;
            console.log("Connected To DataBase!");
        });

        this.insert = new Insert(this.conn);
        this.update = new Update(this.conn);
        this.select = new Select(this.conn);
    }
}

export const DB = new Database("localhost", "root", "", "factorygame");
