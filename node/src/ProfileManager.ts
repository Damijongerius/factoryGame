import { Request, Response } from "express";
export class ProfileManager{

    SaveProfile(req : Request, res : Response){
        console.log(req.body.sendJson);
    }

    loadProfile(req : Request, res : Response){

    }

    deleteProfile(req : Request, res : Response){

    }

    loadStatistics(req : Request, res : Response){

    }
}