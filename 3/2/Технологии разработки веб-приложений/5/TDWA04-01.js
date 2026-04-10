const express = require('express');

const app = express();

const nick = process.argv[2];
const listenPort = Number(process.argv[3]);
const delay = Number(process.argv[4]);

if (!nick || !listenPort || !delay) {
  console.error('Usage: node server.js <Nick> <Port> <Delay>');
  process.exit(1);
}

app.use(express.json());

function getDelay(method) {
  switch (method) {
    case 'GET':
      return delay / 3;
    case 'POST':
      return (2 * delay) / 3;
    case 'PUT':
      return delay;
    case 'DELETE':
      return delay / 4;
    default:
      return 0;
  }
}

app.all('/lb', async (req, res) => {
  const allowed = ['GET', 'POST', 'PUT', 'DELETE'];

  if (!allowed.includes(req.method)) {
    return res.sendStatus(405);
  }

  const wait = getDelay(req.method);

  await new Promise(resolve => setTimeout(resolve, wait));

  res.json({
    Nick: nick,
    Method: req.method,
    Delay: wait
  });
});

app.listen(listenPort, () => {
  console.log(`Server ${nick} running on port ${listenPort} with delay ${delay}`);
});