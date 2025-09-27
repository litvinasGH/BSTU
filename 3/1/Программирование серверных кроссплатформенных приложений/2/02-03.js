var http = require('http');

http.createServer(function (req, resp){
    if (req.url == "/api/name"){
        resp.writeHead(200, {'Content-Type': 'text/plain; charset=utf-8'});
        resp.end("Качинскас Вацловас Вацловавич");
    }
}).listen(5000);

console.log('Server running at http://localhost:5000/');

