import * as EH from "./ErrorHandler";
import { Insert } from "./Insert";
import { Update } from "./Update";
import { Select } from "./Select";
import { Delete } from "./Delete";
const mysql = require("mysql");

export class Database {
    conn: any;
    errorHandler: EH.ErrorHandler;

    insert: Insert;
    update: Update;
    select: Select;
    delete: Delete;

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
        this.delete = new Delete(this.conn);
    }
}

export const DB = new Database("localhost", "root", "", "factorygame");
