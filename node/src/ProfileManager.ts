import { Request, Response } from "express";
export class ProfileManager{

    SaveProfile(req : Request, res : Response){
        console.log("save" + req.body.sendJson + "-" + req.body.GUID);
        
    }

    loadProfile(req : Request, res : Response){
        console.log("load" + req.body.ID, req.body.GUID);
    }

    deleteProfile(req : Request, res : Response){
        console.log("delete" + req.body.GUID + "-" + req.body.SaveName);
    }

    loadStatistics(req : Request, res : Response){
        console.log("loadstats" + req.body.GUID);
    }
}