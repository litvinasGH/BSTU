var http = require('http');
const PORT = 5000;


let appState = "norm";

const server = http.createServer((request, response) => {
    if (request.url === "/") {
        response.writeHead(200, { "content-type": "text/html;charset=utf-8" });
        response.end(`<h1 id="state-display">${appState}</h1>`);
    }
    else {
        response.writeHead(404, { "content-type": "text/html;charset=utf-8" });
        response.end(`<h1>Error:404 Not Found</h1><a href="/">To main<a>`)
    }
}).listen(PORT);

console.log(`Server running at http://localhost:${PORT}`);

process.stdin.setEncoding('utf-8');


if (process.stdin.isTTY) {
  process.stdin.setRawMode(false);
}

process.stdout.write(`${appState}->`);
process.stdin.on('data', (data) => {
  const input = data.trim().toLowerCase();

  switch (input) {
    case 'exit':
      process.exit(0);
      break;

    case 'norm':
    case 'stop':
    case 'idle':
      console.log(`reg = ${appState} --> ${input}`);
      appState = input;
      break;

    default:
      console.log(`${input}`);
      break;
  }

  process.stdout.write(`${appState}->`);
});


