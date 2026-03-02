var http = require('http');
var fs = require( 'fs');

http.createServer(function (req, resp){
    if (req.url == "/html"){
        let html = fs.readFileSync('index.html');
        resp.writeHead(200, {'Content-Type': 'text/html'});
        resp.end(html);
    }
}).listen(5000);

console.log('Server running at http://localhost:5000/html');

