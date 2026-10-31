const express = require('express'); 
const passport = require('passport'); 
const { BasicStrategy } = require('passport-http'); 
const authData = require('./auth-users.json'); 
const app = express(); 
const PORT = 3000;
let isAuthenticated = false;
let id = 0;

passport.use(new BasicStrategy({ realm: `User${id}` }, (username, password, done) => { 
        const user = authData.users.find( u => u.username === username && u.password === password ); 
        if (!user) { 
            return done(null, false); 
        } 
        isAuthenticated = true;
        return done(null, user); 
    })); 

app.use(passport.initialize()); 


app.get('/login', passport.authenticate('basic', { session: false }), (req, res) => { 
    res.redirect('/resource');
}); 


app.get('/resource', (req, res) => { 
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
        .set('WWW-Authenticate', 'Basic realm="logout"')
        .send('Logged out');
}); 


app.use((req, res) => { 
    res.status(404).send('404 Not Found'); 
}); 


app.listen(PORT, () => { 
    console.log(`Server started on http://localhost:${PORT}`);
}); 