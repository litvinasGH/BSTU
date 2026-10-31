const http = require('http');
const { init, send } = require('m06030liti');
const querystring = require('querystring');
const fs = require('fs');
const PORT = 5000;


init(JSON.parse(fs.readFileSync("./config.hide", 'utf8')));

const serverFunction = function (request, response) {
    if (request.url === "/" && request.method === "GET") {
        response.writeHead(200, { 'content-type': 'text/html;charset=utf-8' });
        response.end(fs.readFileSync('./m0603.html'));
    }
    else if (request.url === "/" && request.method === "POST") {

        let content = '';
        request.on('data', chunk => {
            content += chunk.toString();
        });

        request.on('end', () => {
            const { message } = querystring.parse(content);

            if (message) {
                send(message)
                    .then(info => {
                        response.writeHead(200, { 'content-type': 'text/html;charset=utf-8' });
                        response.end("<h1>200 Mail successfully sent</h1>");
                    })
                    .catch(err => {
                        response.writeHead(500, { 'content-type': 'text/html;charset=utf-8' });
                        response.end("<h1>500 Failed to send an email</h1>" +
                        `${err}`);
                    })
            }



        });

    }
    else{
        response.writeHead(404, { 'content-type': 'text/html;charset=utf-8' });
        response.end("<h1>404 Not Found</h1>");
    }
}

const server = http.createServer(serverFunction);

server.listen(PORT);


console.log(`Server running at http://localhost:${PORT}/`);
