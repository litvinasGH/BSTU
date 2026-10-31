const express = require('express');
const session = require('express-session');
const path = require('path');

const app = express();
const PORT = 3000;

const authData = require('./auth-users.json');

function findUser(username) {
  return authData.users.find(u => u.username === username) || null;
}

app.use(express.urlencoded({ extended: false }));


app.use(session({
  secret: 'secret-key',
  resave: false,
  saveUninitialized: false
}));


function checkAuth(req, res, next) {
  if (req.session.user) {
    return next();
  }
  return res.redirect('/login');
}


app.get('/login', (req, res) => {
  if (req.session.user) {
    return res.redirect('/resource');
  }
  res.send(`
    <html>
      <body>
        <h2>Login</h2>
        <form method="POST" action="/login">
          <input name="login" placeholder="login" />
          <input name="password" type="password" placeholder="password" />
          <button type="submit">Login</button>
        </form>
      </body>
    </html>
  `);
});


app.post('/login', (req, res) => {
  const { login, password } = req.body;

  const user = findUser(login);

  if (!user || user.password !== password) {
    return res.send('ERROR');
  }

  req.session.user = { login: user.login };

  res.redirect('/resource');
});


app.get('/resource', checkAuth, (req, res) => {
  res.send('RESOURCE');
});


app.get('/logout', (req, res) => {
  req.session.destroy(() => {
    res.send('LogOut');
  });
});


app.use((req, res) => {
  res.status(404).send('404 Not Found');
});

app.listen(PORT, () => {
  console.log(`http://localhost:${PORT}`);
});
