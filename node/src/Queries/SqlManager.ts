import { Database } from "../objects/Database";
import { Profile } from "../objects/Profile";

export class SqlManager {
  async insertProfile(profile: Profile) {
    const profileQuery = `INSERT INTO profile (SaveName, users_id) VALUES (?, ?)`;
    const profileResult = await Database.query(profileQuery, [
      profile.name,
      profile.userId,
    ]);
    const profileId = profileResult.insertId;

    const mapQuery = `INSERT INTO map (xRange, yRange, profile_id) VALUES (?, ?, ?)`;
    await Database.query(mapQuery, [profile.xRange, profile.yRange, profileId]);

    const statisticsQuery = `INSERT INTO statistics (DateMade, DateSeen, TimePlayed, profile_id, Money) VALUES (?, ?, ?, ?, ?)`;
    await Database.query(statisticsQuery, [
      profile.DateMade,
      profile.DateSeen,
      profile.TimePlayed,
      profileId,
      profile.money,
    ]);

    const objectsQuery = `INSERT INTO objects (position_x, position_y, objectOrder, profile_id) VALUES (?, ?, '?', ?)`;
    for (const gameObject of profile.gameObjects) {
      await Database.query(objectsQuery, [
        gameObject.x,
        gameObject.y,
        gameObject.objectOrder,
        profileId,
      ]);
    }
  }
  
  async getProfile(id : number){
    const sqlQuery = `SELECT
    p.SaveName AS Name,
    p.DateMade,
    p.DateSeen,
    p.TimePlayed,
    s.networth,
    s.Money AS money,
    s.Data AS data,
    m.xRange AS SeedX,
    m.yRange AS SeedY,
    m.size,
    o.position_x AS x,
    o.position_y AS y,
    o.objectOrder AS 'order'
FROM
    profile p
    INNER JOIN statistics s ON p.id = s.profile_id
    INNER JOIN map m ON p.id = m.profile_id
    LEFT JOIN objects o ON m.profile_id = o.profile_id
WHERE
    p.id = ?
ORDER BY
    p.id ASC,
    o.position_x ASC,
    o.position_y ASC;
    `

    const [rows] = await Database.query(sqlQuery, [id]);
    const data = rows.reduce((acc, row) => {
        const profile = {
          Name: row.Name,
          DateMade: row.DateMade,
          DateSeen: row.DateSeen,
          TimePlayed: row.TimePlayed,
          Statistics: {
            networth: row.networth,
            money: row.money,
            data: row.data,
          },
        };
      
        if (!acc.profile || acc.profile.Name !== row.Name) {
          acc.profile = profile;
          acc.map = {
            SeedX: row.SeedX,
            SeedY: row.SeedY,
            size: row.size,
            objects: [],
          };
        }
      
        if (row.x !== null && row.y !== null) {
          acc.map.objects.push({ x: row.x, y: row.y, order: row.order });
        }
      
        console.log(acc);
        return acc;
      }, {});
      console.log(data);
  }

  async deleteProfile(saveName: string, guid: string){
    const sql = `START TRANSACTION;

    SELECT id INTO @profileId
    FROM profile
    WHERE SaveName = ? AND users_id = ?;
    
    DELETE FROM objects
    WHERE profile_id = @profileId;
    
    DELETE FROM map
    WHERE profile_id = @profileId;
    
    DELETE FROM statistics
    WHERE profile_id = @profileId;
    
    DELETE FROM profile
    WHERE id = @profileId;
    
    COMMIT;`

    return await Database.query(sql, [saveName,guid]);
  }

  async insertUser(guid: string, username: string, password: string) {
    username = username.toLowerCase();
    const sqlQuery = `INSERT INTO users (id,username, password) VALUES (?, ?, ?)`;

    await Database.query(sqlQuery, [guid, username, password]);
  }

  async getUserWithName(username: string) {
    username = username.toLowerCase();
    const sqlQuery = `SELECT * FROM users WHERE UserName = ?`;

    return await Database.query(sqlQuery, [username]);
  }

  async deleteUser(guid: string) {
    const sqlQuery = `
        DELETE FROM map
        WHERE profile_id IN (
          SELECT id
          FROM profile
          WHERE users_id = '${guid}'
        );
        
        DELETE FROM statistics
        WHERE profile_id IN (
          SELECT id
          FROM profile
          WHERE users_id = '${guid}'
        );
        
        DELETE FROM profile
        WHERE users_id = '${guid}'
        
        DELETE FROM users
        WHERE 
        id = '${guid}'`;

    return await Database.query(sqlQuery);
  }

  async getProfiles(guid: string) {
    const sqlQuery = `Select * FROM profile WHERE users_id = '${guid}'`;

    return await Database.query(sqlQuery);
  }

  async getStatistics(guid: string) {
    const sqlQuery = `SELECT * FROM statistics WHERE profile_id = '${guid}'`;

    return await Database.query(sqlQuery);
  }
}
