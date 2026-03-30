const express = require('express'); 
const passport = require('passport'); 
const { DigestStrategy } = require('passport-http'); // 👈 изменили
const authData = require('./auth-users.json'); 
const app = express(); 
const PORT = 3000;

let isAuthenticated = false;

passport.use(new DigestStrategy(
    { qop: 'auth' },
    (username, done) => {
        const user = authData.users.find(
            u => u.username === username
        );

        if (!user) {
            return done(null, false);
        }

        isAuthenticated = true;
        return done(null, user, user.password); 
    },
    (params, done) => {
        return done(null, true);
    }
));

app.use(passport.initialize()); 

app.get('/login', passport.authenticate('digest', { session: false }), (req, res) => { 
    res.redirect('/resource');
}); 

app.get('/resource', passport.authenticate('digest', { session: false }), (req, res) => { 
    if (isAuthenticated){
        res.send('RESOURCE'); 
    }
    else {
        res.redirect('/login');
    }
}); 

app.get('/logout', (req, res) => { 
    isAuthenticated = false;
    res
        .status(401)
        .set('WWW-Authenticate', 'Digest realm="logout", qop="auth", nonce="' + Date.now() + '"')
        .send('Logged out');
}); 

app.use((req, res) => { 
    res.status(404).send('404 Not Found'); 
}); 

app.listen(PORT, () => { 
    console.log(`Server started on http://localhost:${PORT}`);
});