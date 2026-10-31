var fs = require('fs');
const path = require("path");

const mimeTypes = {
    'html': 'text/html',
    'css': 'text/css',
    'js': 'text/javascript',
    'png': 'image/png',
    'docx': 'application/msword',
    'json': 'application/json',
    'xml': 'application/xml',
    'mp4': 'video/mp4'
};

function createStaticHandler(staticDirectory) {

    return function staticHandler(request, response) {

        if (request.method !== "GET") {
            response.writeHead(405, {'Content-Type': 'text/plain'});
            response.end('Method Not Allowed');
            return;
        }

        let filePath = decodeURI(request.url);
        if (filePath === '/') filePath = '/index.html';

        const absPath = path.join(process.cwd(), staticDirectory, filePath);

        if (!fs.existsSync(absPath)) {
            response.writeHead(404, {'Content-Type': 'text/plain'});
            response.end('Not Found');
            return;
        }
        const extension = filePath.split('.').pop();
        const mime = mimeTypes[extension];

        response.writeHead(200, {'Content-Type': mime});
        fs.createReadStream(absPath).pipe(response);
    };
}

module.exports = createStaticHandler;