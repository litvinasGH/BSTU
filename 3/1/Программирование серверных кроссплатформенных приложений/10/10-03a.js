const ws = require('ws');
const wss = new ws('ws://localhost:5000/broadcast');

const clientId = process.pid;

wss.on('open',()=>{
    wss.on('message',(message)=>{
        console.log(`Message: ${message}`);
        wss.send(`Hello from client: ${clientId}`);
    });
});