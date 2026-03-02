var http = require('http');
var fs = require( 'fs');

http.createServer(function (req, resp){
    if (req.url == "/jquery"){
        let html = fs.readFileSync('jquery.html');
        resp.writeHead(200, {'Content-Type': 'text/html'});
        resp.end(html);
    }
    else if (req.url == "/api/name"){
        resp.writeHead(200, {'Content-Type': 'text/plain; charset=utf-8'});
        resp.end("Качинскас Вацловас Вацловавич");
    }
}).listen(5000);

console.log('Server running at http://localhost:5000/jquery');

