const ws = require('ws');
const socket = new ws.WebSocket('ws:/localhost:4000/wsserver');

let counter = 0;

socket.on('open', () => {
    counter = 0;
    setInterval(() => { socket.send(`10-01-client: ${++counter}`); }, 3000);

    setTimeout(() => {
        socket.close();
    }, 25000);

    socket.onclose = (e) => {
        console.log('Close connection');
        process.exit(0);
    }
    socket.onmessage = (e) => {
        console.log('Message: ', e.data);
    }
    socket.onerror = function (error) {
        console.error("Error: ", error.message);
    }
});
