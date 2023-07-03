import { Request, Response } from "express";
import { SqlManager } from './Queries/SqlManager';
import { Profile } from "./objects/Profile";

const sqlManager = new SqlManager();

export class ProfileManager{

    SaveProfile(req : Request, res : Response){
        console.log("save" + req.body.sendJson + "-" + req.body.GUID);
        const profile = new Profile(req.body.sendJson, req.body.GUID);
        sqlManager.insertProfile(profile);

        res.send({status: 1, message: "profile inserted successfully"});
    }
    
    async loadProfile(req : Request, res : Response){
        console.log("load" + req.body.ID, req.body.GUID);
        res.send(await sqlManager.getProfile(req.body.ID));
    }

    async deleteProfile(req : Request, res : Response){
        console.log("delete" + req.body.GUID + "-" + req.body.SaveName);
        await sqlManager.deleteProfile(req.body.SaveName, req.body.GUID);
        res.send({status: 1, message: "profile deleted successfully"});
    }

    async loadStatistics(req : Request, res : Response){
        console.log("loadstats" + req.body.GUID);

        const profiles : any[] = await sqlManager.getProfiles(req.body.GUID);
        let stats : any[] = [];
        for (const profile of profiles){
            const stat : any[] = await sqlManager.getStatistics(profile.id);
            stats.push(stat[0]);
        }

        res.send({profiles : profiles, statistics : stats});
    }
}