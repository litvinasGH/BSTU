const express = require('express');
const fs = require('fs');
const path = require('path');
const passport = require('passport');
const { DigestStrategy } = require('passport-http');

const PORT = 3000;
const app = express();

const authData = require('./auth-users.json');

let isAuthenticated = false;

function findUser(username) {
  return authData.users.find(u => u.username === username) || null;
}
passport.use(new DigestStrategy(
  { qop: 'auth' },
  function (login, done) {
    const user = findUser(login);
    if (!user) {
      return done(null, false);
    }
    return done(null, { login: user.login }, user.password);
  },
  function (params, done) {
    return done(null, true);
  }
));

app.use(passport.initialize());

app.get('/login', passport.authenticate('digest', { session: false }), (req, res) => {
    isAuthenticated = true;
    res.redirect('/resource');
});

app.get('/resource', (req, res) => {
    if (isAuthenticated){
        res.send('RESOURCE'); 
    }
    else {
        res.redirect('/login');
    }
  }
);


app.get('/logout', (req, res) => {
  res
    .status(401)
    .set('WWW-Authenticate', 'Digest realm="Users", qop="auth", nonce="' + Date.now() + '", opaque="logout"')
    .send('Logged out');
});

app.use((req, res) => {
  res.status(404).send('404 Not Found'); 
});

app.listen(PORT, () => {
  console.log(`Server listening at http://localhost:${PORT}`);
});
