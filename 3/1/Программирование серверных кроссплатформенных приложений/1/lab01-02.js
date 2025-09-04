var http = require('http');

http.createServer(function (req, resp){
    resp.writeHead(200, {'Content-Type': 'text/html'});
    resp.end('<h1>Hello World!</h1>\n')
}).listen(3000);

console.log('Server running at http://localhost:3000/');