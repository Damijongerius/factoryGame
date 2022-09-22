//node module express
const { response, request } = require("express");

const express = require("express");

const app = express();

app.use(express.json());

//node module body parser
const bodyParser = require("body-parser");

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json({ reviver: true }));

app.get("/ping", (req, res) => {
  res.send("pong");
});
app.listen(3000, function () {
  console.log("server draai");
});
app.post("/senddata", function (req, res) {
  let a = req.body;
  console.log(a);

  let json = req.body;
  console.log(json);

  if (json instanceof String) {
    let object = json;
    console.log(data.profile.Statistics.money);
  }

  res.send("Dami is gay");
});
