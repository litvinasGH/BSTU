var http = require('http');
var fs = require('fs');

http.createServer(function (req, res){
    if(req.url == "/png"){
        const name = 'image/pic.png';
        let img = null;
        fs.stat(name, (err, stat)=>{
            if(err){console.log('error:', err);}
            else{
                img = fs.readFileSync(name);
                res.writeHead(200, {'Content-Type': 'image/png', 'Content-Length':stat.size})
                res.end(img,'binary');
            }
        })
    }

}).listen(5000);

console.log('Server running at http://localhost:5000/');
