const ws = require('ws');
const PORT = 4000;
const MESSAGE_INTERVAL = 15000;
const PING_INTERVAL = 5000;
const PONG_WAIT = 200;
const SERVER_OPTIONS = {
    port: PORT,
    host: 'localhost',
    path: '/broadcast'
}
let messagesCounter = 0;
const server = new ws.Server(SERVER_OPTIONS);

server.on('connection', (socket) => {
    const aliveMap = new Map();
    aliveMap.set(socket, true);

    socket.on('pong', (data) => {
        aliveMap.set(socket, true);
        console.log('Client pong: ', data.toString());
    });

    socket.on('close', () => {
        aliveMap.delete(socket);
        console.log('client disconnected');
    });

    socket.on('error', (err) => {
        aliveMap.delete(socket);
        console.error(`Error occured: ${err}`);
    });
    const pingInterval = setInterval(() => {
        if (socket.readyState !== ws.OPEN) {
            clearInterval(pingInterval);
            return;
        }

        const alive = aliveMap.get(socket);
        if (alive === false) {
            console.log('Terminating unresponsive client');
            clearInterval(pingInterval);
            aliveMap.delete(socket);
            return socket.terminate();
        }

        aliveMap.set(socket, false);
        socket.ping();
    }, PING_INTERVAL);

    socket.on('close', () => {
        clearInterval(pingInterval);
    });

    socket.on('error', () => {
        clearInterval(pingInterval);
    });
});

setInterval(() => {
    server.clients.forEach((client) => {
        if (client.readyState == ws.OPEN) {
            client.send(`11-03-Server: ${++messagesCounter}`);
        }
    });
}, MESSAGE_INTERVAL);

console.log(`Server listening at ws://${SERVER_OPTIONS.host}:${SERVER_OPTIONS.port}${SERVER_OPTIONS.path}`);