import { Database } from "../objects/Database";

export class SqlManager{
    async insertProfile(Profile){

    }

    async insertUser(guid: string, username : string, password : string){
        const sqlQuery = `INSERT INTO users (UserId,username, password) VALUES (?, ?,?)`

        Database.query(sqlQuery, [guid,username, password]);
    }
}