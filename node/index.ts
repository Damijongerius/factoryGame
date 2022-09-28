const { DB } = require("./DB/Database");
//node module express
const { response, request, json } = require("express");

const express = require("express");

const app = express();

app.use(express.json());

//node module body parser
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

  //data = JSON.parse(req.body);

  console.log(a.map.grid);
});

app.post("/recieve", function (req,res) {
  let string = JSON.parse(req.body.sendJson);

  
})
