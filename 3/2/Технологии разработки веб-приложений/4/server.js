const express = require('express');

const app = express();

const nick = process.argv[2];
const listenPort = Number(process.argv[3]);

if (!nick || !listenPort) {
  console.error('Usage: node server.js <Nick> <Port>');
  process.exit(1);
}

app.use(express.json());

function handler(req, res) {
  res.json({
    Nick: nick,
    Method: req.method
  });
}


app.all("/A", (req, res) => {
  const allowed = ['GET', 'POST', 'PUT', 'DELETE'];

  if (!allowed.includes(req.method)) {
    return res.sendStatus(405);
  }

  return handler(req, res);
});

app.listen(listenPort, () => {
  console.log(`Server started on port ${listenPort}`);
});