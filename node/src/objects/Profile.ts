
export class profile{
    userId : string;
    id : number;

    DateMade : string;
    DateSeen : string;
    TimePlayed : string;

    xRange : number;
    yRange : number;

    gameObjects : GameObject[];

    constructor(jsonObject : any){
        this.userId = jsonObject.userId;
        this.id = jsonObject.id;
        this.DateMade = jsonObject.dateMade;
        this.DateSeen = jsonObject.dateSeen;
        this.TimePlayed = jsonObject.timePlayed;
        this.xRange = jsonObject.xRange;
        this.yRange = jsonObject.yRange;
        
    }
}

class GameObject {
    x : number;
    y : number;

    objectOrder : String;
}