import { Database } from "../objects/Database";

export class SqlManager{
    async insertProfile(Profile){

    }

    async insertUser(guid: string, username : string, password : string){
        username = username.toLowerCase();
        const sqlQuery = `INSERT INTO users (id,username, password) VALUES (?, ?, ?)`

        Database.query(sqlQuery, [guid,username, password]);
    }

    async getUserWithName(username : string){
        username = username.toLowerCase();
        const sqlQuery = `SELECT * FROM users WHERE UserName = ?`;

        return await Database.query(sqlQuery, [username]);
    }

    async deleteUser(guid : string){
        const sqlQuery = `
        DELETE FROM map
        WHERE profile_id IN (
          SELECT id
          FROM profile
          WHERE users_id = ${guid}
        );
        
        DELETE FROM statistics
        WHERE profile_id IN (
          SELECT id
          FROM profile
          WHERE users_id = ${guid}
        );
        
        DELETE FROM profile
        WHERE users_id = ${guid}
        
        -- Delete the user from the users table
        DELETE FROM users
        WHERE 
        id = ${guid}`;

        return Database.query(sqlQuery, [guid]);
    }
}