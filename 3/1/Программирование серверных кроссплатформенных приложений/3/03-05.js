var http = require('http');
var url = require('url');
var fs = require('fs');
const PORT = 5000;

function factorial(n, callback) {
    if (isNaN(n) || n < 0) {
        return callback(null);
    }
    if (n === 0) {
        return callback(1);
    }

    let result = 1;

    function calculate(num) {
        result *= num;
        if (num > 1) {
            setImmediate(() => calculate(num - 1));
        }
        else {
            callback(result);
        }
    }
    calculate(n);
}


const server = http.createServer(function (request, response) {
    const parsedUrl = url.parse(request.url, true);
    const parsedNum = parseInt(parsedUrl.query.k);

    if (request.method==="GET"&&parsedUrl.pathname==="/fact"&&!isNaN(parsedNum)) {
        factorial(parsedNum,(processedNum)=>{
            if(processedNum!=null){
                response.writeHead(200,{'content-type':'application/json;charset=utf-8'});
                response.end(JSON.stringify(
                    {
                        k:parsedNum,
                        fact:processedNum
                    }
                ))
            }
            else{
                response.writeHead(400,{'content-type':'application/json;charset=utf-8'});
                response.end(JSON.stringify({
                    error:"Invalid factorial number"
                }))
            }
        })
    }
    else if(request.method==="GET"&&request.url==="/fact"){
        let html = fs.readFileSync("./fact.html");
        response.writeHead(200,{'content-type':'text/html'});
        response.end(html);
    }
    else if(request.method==="GET"&&request.url==="/test"){
        let html = fs.readFileSync("./test.html");
        response.writeHead(200,{'content-type':'text/html'});
        response.end(html);
    }
    else{
        response.writeHead(404,{'content-type':'text/html'});
        response.end(
            '<h1>404 Not Found</h1>'
        );
    }
}).listen(PORT);

console.log(`Server running at http://localhost:${PORT}/fact`);

