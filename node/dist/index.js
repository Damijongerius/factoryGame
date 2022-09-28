"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const { DB } = require("./DB/Database");
const { saveFile } = require("./models/SaveFile");
const { response, request, json } = require("express");
const express = require("express");
const app = express();
app.use(express.json());
let data;
let database;
const bodyParser = require("body-parser");
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.get("/ping", (req, res) => {
    res.send("pong");
});
app.listen(3000, function () {
    console.log("server draai");
});
app.post("/senddata", function (req, res) {
    console.log(req.body);
    let a = JSON.parse(req.body.sendJson);
    console.log(saveFile.profile.Statistics.money);
    saveFile.profile = new Profile();
    saveFile.profile.Statistics = new Statistics();
    //saveFile.Profile.Statistics.money = a.profile.Statistics.money;
    //data = JSON.parse(req.body);
    // console.log(a.map.grid[0]);
});
//# sourceMappingURL=index.js.map