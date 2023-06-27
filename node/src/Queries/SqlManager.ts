
export class sqlManager{

    async insertProfile(){
        const Query = `START TRANSACTION;

        INSERT INTO `factorygame`.`users` (`UserName`, `password`)
        VALUES ('JohnDoe', 'password123');
        
        INSERT INTO `factorygame`.`savefile` (`SaveName`, `users_UserId`)
        VALUES ('MySaveFile', LAST_INSERT_ID());
        
        INSERT INTO `factorygame`.`map` (`xRange`, `yRange`, `savefile_ID`)
        VALUES (10.5, 20.5, LAST_INSERT_ID());
        
        INSERT INTO `factorygame`.`profile` (`DateMade`, `DateSeen`, `TimePlayed`, `savefile_ID`, `Money`)
        VALUES ('2023-06-27 12:34:56', NULL, '2 hours', LAST_INSERT_ID(), 500);
        
        INSERT INTO `factorygame`.`objects` (`position_x`, `position_y`, `objectOrder`, `map_savefile_ID`)
        VALUES (5, 5, 'object1', LAST_INSERT_ID());
        
        COMMIT;`
    }
}