var http = require("http");
var fs = require("fs");
const url = require("url");
const open = require("open");
const { Console } = require("console");

const port = 5001;
const headers = { "Content-Type": "application/json" };

function options(req, res) {
  res.writeHead(204);
  return res.end();
}

function get(req, res) {
  fs.readFile(`mock-server${url.parse(req.url, true).pathname}.json`, (err, data) => {
    if (err) {
      res.writeHead(404, headers);
      return res.end("404 Not Found");
    }
    res.writeHead(200, headers);
    res.write(data);
    return res.end();
  });
}

function post(req, res) {
  fs.readFile(`mock-server${url.parse(req.url, true).pathname}.post.json`, (err, data) => {
    res.writeHead(200, headers);
    if (!err) {
      res.write(data); // return the json file if found
    }
    return res.end();
  });
}

function put_delete(req, res) {
  res.writeHead(200, headers);
  return res.end();
}

function delay(time) {
  return new Promise(resolve => setTimeout(resolve, time));
}

http
  .createServer(function (req, res) {
    // headers for cors
    res.setHeader("Access-Control-Allow-Origin", "*");
    res.setHeader("Access-Control-Request-Method", "*");
    res.setHeader("Access-Control-Allow-Methods", "OPTIONS, GET, POST");
    res.setHeader("Access-Control-Allow-Headers", "*");

    console.log(`${req.url} (${req.method})`);
    switch (req.method.toUpperCase()) {
      case "OPTIONS":
        return options(req, res);
      case "GET":
        // modify the delay value to mimic real server as needed
        return delay(10).then(() => get(req, res));
      case "POST":
        return post(req, res);
      default:
        return put_delete(req, res);
    }
  })
  .listen(port);

// open(`http://localhost:${port}`);
console.log(`** server is listening on http://localhost:${port} **`);
