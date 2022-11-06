// // \\ // \\ // \\
import { SaveFile, Convert, Profile, Map } from "./models/SaveFile";
import { DB } from "./DB/Database";
import express from "express";
import bcrypt from "bcrypt";
import bodyParser from "body-parser";
import { profile } from "console";
// \\ // \\ // \\ //

// // \\ // \\ // \\
const app = express();

app.use(express.json());

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
// \\ // \\ // \\ //

// onApplication start
// // \\ // \\ // \\
app.listen(3000, function () {
    console.log("server running");
});
// \\ // \\ // \\ //

// OnSaveSaveFileRequest
// // \\ // \\ // \\
app.post("/Save/savefile", async function (req, res) {
    const saveFile: SaveFile = Convert.toSaveFile(req.body.sendJson);
    console.log(saveFile);
    const { map, profile } = saveFile;
    const result = await DB.insert.SaveFile(saveFile, req.body.GUID);
    console.log(result);
    DB.insert.Profile(profile, result);
    DB.insert.statistics(profile.Statistics, result);
    DB.insert.Map(map, result);
    map.grid.forEach((element) => {
        DB.insert.Cell(element, result);
        DB.insert.Objinfo(element.ObjInfo, element.x, element.y, result);
    });
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
    if (GUID !== null) {
        if (ID instanceof Array<number>) {
            console.log("alot of ID's");
            const sfs = [];
            for (const sfid of ID) {
                const sf = await GenerateSaveFile(sfid);
                sfs.push(sf);
            }
            res.send(sfs);
        } else if (ID !== null) {
            const sf = await GenerateSaveFile(ID);
            res.send(sf);
        } else {
            res.send({ status: 13, message: "need valid ID" });
        }
    } else {
        res.send({ status: 11, message: "need valid GUID" });
    }

    async function GenerateSaveFile(ID: number) {
        console.log("generating sf");
        const sf: any = await DB.select.SaveFile(ID);
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
        const nstatistics = await DB.select.statistics(ID);
        const statistics = {
            networth: nstatistics[0].networth,
            money: nstatistics[0].money,
            data: nstatistics[0].data,
            xp: nstatistics[0].xp,
            Level: nstatistics[0].Level,
        };
        profile.Statistics = statistics;
        const ncells = await DB.select.Cell(ID);
        for (const idx in ncells) {
            const cells = {
                x: ncells[idx].x,
                y: ncells[idx].y,
                objType: ncells[idx].ObjInfo,
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

//onUserCreateRequest
// // \\ // \\ // \\
app.post("/CreateUser", async function (req, res) {
    const asyncHash = await EncryptPasswordASync(req.body.Password);
    DB.insert.User(
        req.body.GUID,
        req.body.UserName,
        asyncHash,
        function (info: object) {
            res.send(JSON.stringify(info));
        }
    );
});
// \\ // \\ // \\ //

//onUserLoadRequest
// // \\ // \\ // \\
app.post("/LoadUser", async function (req, res) {
    //needs guid username and password
    const data: any = {
        UserName: req.body.UserName,
        Password: req.body.Password,
    };
    if (req.body.GUID !== null) {
        data.GUID = req.body.GUID;
    }
    const info: any = await DB.select.User(data);
    switch (info.status) {
        case 3: {
            res.send({
                Status: 3,
                message: `no existing user with name ${data.UserName}`,
            });
            break;
        }

        case 2: {
            console.log(info.result);
            info.result.forEach(async function (i, idx, array) {
                comparePassword(
                    req.body.Password,
                    array[idx].password,
                    function (params: boolean) {
                        if (params) {
                            res.send({
                                Status: 4,
                                message: `the password matches ${data.UserName}`,
                                Info: {
                                    UserName: array[idx].UserName,
                                    GUID: array[idx].UserId,
                                },
                            });
                        } else {
                            if (i === array.length - 1) {
                                res.send({
                                    Status: 5,
                                    message: "incorrect password try again",
                                });
                            }
                        }
                    }
                );
            });
            break;
        }
        default: {
            console.log("error");
            res.send({
                Status: 101,
                message: "error on our end",
            });
            break;
        }
    }
});
// \\ // \\ // \\ //

//onUserDeleteRequest
// // \\ // \\ // \\
app.post("/DeleteUser", function (req, res) {
    //removes player with savefiles
    //deletes profile with saved guid
});
// \\ // \\ // \\ //

// encryption
// // \\ // \\ // \\
async function EncryptPasswordASync(password) {
    const hash = await bcrypt.hash(password, 10);
    return hash;
}

async function comparePassword(
    password: string | Buffer,
    hash: string,
    callback: Function
) {
    callback(await bcrypt.compare(password, hash));
}
// \\ // \\ // \\ //
