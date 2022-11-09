import {
    Profile,
    SaveFile,
    Statistics,
    Map,
    cells,
    ObjInfo,
} from "../models/SaveFile";

export class Select {
    conn: any;
    constructor(conn: any) {
        this.conn = conn;
    }

    sf(GUID: string, SaveName:string): any {
        const sql = `SELECT * FROM savefile WHERE users_UserId = "${GUID}" AND SaveName = "${SaveName}"`;

        return new Promise<Array<SaveFile>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    SaveFiles(GUID: string): any {
        const sql = `SELECT * FROM savefile WHERE users_UserId = "${GUID}"`;

        return new Promise<Array<SaveFile>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    SaveFile(ID: number, GUID: string): Promise<Array<SaveFile>> {
        const sql = `SELECT * FROM savefile WHERE ID = "${ID}" AND users_UserId = "${GUID}"`;

        return new Promise<Array<SaveFile>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    Profile(saveFileID: number): Promise<Array<Profile>> {
        const sql = `SELECT * FROM profile WHERE savefile_ID = ${saveFileID}`;

        return new Promise<Array<Profile>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    statistics(saveFileId: number): Promise<Array<Statistics>> {
        const sql = `SELECT * FROM statistics WHERE savefile_ID = ${saveFileId}`;

        return new Promise<Array<Statistics>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    Map(saveFileId: number): Promise<Array<Map>> {
        const sql = `SELECT * FROM map WHERE savefile_ID = ${saveFileId}`;

        return new Promise<Array<Map>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    Cell(saveFileId: number): Promise<Array<cells>> {
        const sql = `SELECT * FROM cells WHERE savefile_ID = ${saveFileId}`;

        return new Promise<Array<cells>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    Objinfo(x: number, y: number, saveFileId: number): Promise<Array<ObjInfo>> {
        const sql = `SELECT * FROM objectinfo WHERE cells_x = ${x} AND cells_y = ${y}  AND savefile_ID  = ${saveFileId}`;

        return new Promise<Array<ObjInfo>>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                return err ? reject(err) : resolve(result);
            });
        });
    }

    User(data: any) {
        let sql;
        console.log("run sql");
        if (data.GUID == null) {
            console.log("UserName");
            sql = `SELECT * FROM users WHERE UserName = "${data.UserName}" `;
        } else {
            console.log("GUID");
            sql = `SELECT * FROM users WHERE UserId = "${data.GUID}"`;
        }

        return new Promise<any>((resolve, reject) => {
            this.conn.query(sql, (err, result) => {
                if (err) {
                    reject({ status: 3, message: "Was not able to find user" });
                    console.log(err);
                } else {
                    resolve({
                        status: 2,
                        message: "This is what i found",
                        result: result,
                    });
                }

                console.log(result);
            });
        });
    }
}
