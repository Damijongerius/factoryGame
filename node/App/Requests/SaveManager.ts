

// OnSaveSaveFileRequest

import { app } from "../..";
import { DB } from "../DB/Database";
import { Convert, Profile, SaveFile } from "../models/SaveFile";

// // \\ // \\ // \\
app.post("/Save/savefile", async function (req, res) {
    const saveFile: SaveFile = Convert.toSaveFile(req.body.sendJson);
    const { map, profile } = saveFile;
  
    const savedFile: any = await DB.select.sf(
      req.body.GUID,
      saveFile.profile.Name
    );
    console.log(savedFile);
    if (savedFile.length == 0) {
      const result = await DB.insert.SaveFile(saveFile, req.body.GUID);
      console.log(result);
      DB.insert.Profile(profile, result);
      DB.insert.statistics(profile.Statistics, result);
      DB.insert.Map(map, result);
      map.grid.forEach((element) => {
        DB.insert.Cell(element, result);
        DB.insert.Objinfo(element.ObjInfo, element.x, element.y, result);
      });
    } else {
      const { ID } = savedFile[0];
  
      DB.update.Profile(profile, ID);
      DB.update.statistics(profile.Statistics, ID);
  
      const savedCells: any = await DB.select.Cell(ID);
      const copy = saveFile.map.grid;
      for (const cell of savedCells) {
        for (const cc of saveFile.map.grid) {
          if (cc.x == cell.x && cell.y == cc.y) {
            DB.update.Cell(cc, ID);
            DB.update.Objinfo(cc.ObjInfo, cc.x, cc.y, ID);
  
            const index = copy.indexOf(cc);
            console.log(cc);
            copy.splice(index);
          }
        }
      }
      for (const cell of copy) {
        console.log(cell);
        DB.insert.Cell(cell, ID);
        DB.insert.Objinfo(cell.ObjInfo, cell.x, cell.y, ID);
      }
    }
  });
  // \\ // \\ // \\ //
  
  // onloadProfileRequest
  // // \\ // \\ // \\
  app.post("/load/profiles", async function (req, res) {
    if (req.body.GUID !== null) {
      const result: any = await DB.select.SaveFiles(req.body.GUID);
      console.log(result);
      if (result !== null) {
        const respond: Array<Profile> = new Array<Profile>();
        for (const rs of result) {
          const profiles = await DB.select.Profile(rs.ID);
          respond.push(profiles[0]);
          console.log(profiles);
        }
        res.send({ profiles: respond, saveFiles: result });
      } else {
        res.send({ status: 101, message: "unknown ERROR" });
      }
    } else {
      res.send({ status: 11, message: "need valid GUID" });
    }
  });
  // \\ // \\ // \\ //
  
  // onloadSaveFilesRequest
  // // \\ // \\ // \\
  app.post("/Load/savefile", async function (req, res) {
    const ID = req.body.ID;
    const GUID = req.body.GUID;
    if (ID != null) {
      const sf = await GenerateSaveFile(ID, GUID);
      console.log(sf);
      res.json(sf);
    } else {
      res.send({ status: 13, message: "need valid ID" });
    }
  
    async function GenerateSaveFile(ID: number, GUID: string) {
      console.log("generating sf");
      const sf: any = await DB.select.SaveFile(ID, GUID);
      const nprofile = await DB.select.Profile(ID);
      const profile = {
        Name: sf[0].SaveName,
        DateMade: nprofile[0].DateMade,
        DateSeen: nprofile[0].DateSeen,
        TimePlayed: nprofile[0].TimePlayed,
        Statistics: null,
      };
      const nmap = await DB.select.Map(ID);
      const map = {
        xRange: nmap[0].xRange,
        yRange: nmap[0].yRange,
        grid: [],
      };
      const nstatistics: any = await DB.select.statistics(ID);
      const statistics = {
        networth: nstatistics[0].Networth,
        money: nstatistics[0].Money,
        data: nstatistics[0].Data,
        xp: nstatistics[0].Xp,
        Level: nstatistics[0].Level,
      };
      profile.Statistics = statistics;
      const ncells: any = await DB.select.Cell(ID);
      console.log(ncells);
      for (const idx in ncells) {
        console.log(ncells[idx].ObjectTypes);
        const cells = {
          x: ncells[idx].x,
          y: ncells[idx].y,
          objType: ncells[idx].ObjectTypes,
          ObjInfo: null,
        };
        const nObjInfo = await DB.select.Objinfo(
          ncells[idx].x,
          ncells[idx].y,
          ID
        );
        const ObjInfo = {
          exitPoints: nObjInfo[0].exitPoints,
          powered: nObjInfo[0].powered,
          dataStored: nObjInfo[0].dataStored,
          powerStored: nObjInfo[0].powerStored,
          level: nObjInfo[0].level,
          age: nObjInfo[0].age,
          upkeepCost: nObjInfo[0].upkeepCost,
          dataMined: nObjInfo[0].dataMined,
          dataSold: nObjInfo[0].dataSold,
          dataTransferd: nObjInfo[0].dataTransferd,
          Prio: nObjInfo[0].Prio,
          SelfPrio: nObjInfo[0].SelfPrio,
          updateSpeed: nObjInfo[0].updateSpeed,
        };
        cells.ObjInfo = ObjInfo;
        map.grid.push(cells);
      }
  
      const saveFile: SaveFile = { map, profile };
      return saveFile;
    }
  });
  // \\ // \\ // \\ //