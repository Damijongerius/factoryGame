
export class Profile{
    name : string;
    userId : string;

    DateMade : string;
    DateSeen : string;
    TimePlayed : string;
    networth : number;
    money : number;
    data : number;

    xRange : number;
    yRange : number;

    gameObjects : GameObject[];

    constructor(jsonObject : any, guid : string){
        this.userId = guid;

        console.log(jsonObject);
        const json = JSON.parse(jsonObject);

        console.log(json.profile);
        console.log(json);
        console.log(json.profile.DateMade);

        this.DateMade = json.profile.DateMade;
        this.DateSeen = json.profile.DateSeen;
        this.TimePlayed = json.profile.TimePlayed;
        this.xRange = json.map.SeedX;
        this.yRange = json.map.SeedY;
        this.money = json.profile.Statistics.money;
        this.networth = json.profile.Statistics.networth
        this.data = json.profile.Statistics.Data
        this.gameObjects = json.map.objects;
        this.name = json.profile.Name
    }
}

/* {
            profile : 
            {
                Name: "string",
                DateMade: "string",
                DateSeen: "string",
                TimePlayed: "string",
                Statistics:
                {
                    networth: integer,
                    money: integer,
                    data: integer
                }
            },
            map : 
            {
                SeedX: float,
                SeedY: float,
                size: float,
                objects: 
                [
                    {x: float, y: float, order: string}
                ]
            }
        }*/

class GameObject {
    x : number;
    y : number;

    objectOrder : String;
}