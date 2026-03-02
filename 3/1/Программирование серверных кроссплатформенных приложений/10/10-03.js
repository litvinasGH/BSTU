const ws = require('ws');
const socketOptions = {
    port: 5000,
    host: 'localhost',
    path: '/broadcast'
};

const wss = new ws.Server(socketOptions);

wss.on('connection', (socket) => {
    socket.on('message', (message) => {
        console.log(`Message: ${message}`);
    });
});


setInterval(() => {
    wss.clients.forEach((client) => {
        if (client.readyState == ws.OPEN) {
            client.send("Hello from server");
        }
    });
}, 5000);

console.log(`Server running at http://${socketOptions.host}:${socketOptions.port}${socketOptions.path}`);