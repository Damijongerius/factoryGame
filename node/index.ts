// // \\ // \\ // \\
import { SaveFile, Convert, Profile, Map } from "./App/models/SaveFile";
import { DB } from "./App/DB/Database";
import express from "express";
import bcrypt from "bcrypt";
import bodyParser from "body-parser";
var abbrev = require("abbrev");
// \\ // \\ // \\ //

// // \\ // \\ // \\
export const app = express();

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
