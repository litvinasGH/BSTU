var http = require('http');
var fs = require('fs');

http.createServer(function (req, res){
    if(req.url == "/png"){
        const name = 'image/pic.png';
        let img = null;
        img = fs.readFileSync(name);
        res.writeHead(200, {'Content-Type': 'image/png'})
        res.end(img);
            
        
    }

}).listen(5000);

console.log('Server running at http://localhost:5000/png');
