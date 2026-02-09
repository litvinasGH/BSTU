const WebSocket = require('ws');
const ws = new WebSocket('ws://localhost:4000/broadcast');

const CLIENT_NAME = process.pid;

ws.on('open', () => console.log('connected'));
ws.on('message', m => console.log('Message:', m.toString()));
ws.on('ping', (data) => {
  console.log('received ping:', data ? "" : data);
  ws.pong(`Pong: ${CLIENT_NAME}`);
});
ws.on('close', () => console.log('closed'));
ws.on('error', console.error);