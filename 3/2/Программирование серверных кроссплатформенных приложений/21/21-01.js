const express = require('express');
const session = require('express-session');
const fs = require('fs');
const path = require('path');
const pt = require('passport');
const ptHTTP = require('passport-http');
const bodyParser = require('body-parser');
const creds = require('./credentials.json');
const PORT = 3000;

function findUser(login) {
    return creds.users.find(u => u.login == login) || null;
}

const app = express();

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use(express.static(path.join(__dirname,'public')));

app.use(session({
    secret: 'koi carp',
    resave: false,
    saveUninitialized: false
}));

app.use(pt.initialize());
app.use(pt.session());

pt.use(new ptHTTP.BasicStrategy(
    function (login, password, done) {
        const user = findUser(login);
        if (!user) {
            return done(null, false);
        }
        if (user.password != password) {
            return done(null, false);
        }
        return done(null, { login: user.login });
    }
));

pt.serializeUser(function (user, done) {
    done(null, user.login);
});
pt.deserializeUser(function (login, done) {
    const user = creds.users.find(u => u.login == login) || null;
    if (!user) {
        return done(null, false);
    }
    done(null, { login: user.login });
});


function ensureAuthenticated(req, res, next) {
    if (req.isAuthenticated && req.isAuthenticated()) {
        next();
    }

    pt.authenticate('basic', { session: false }, function (err, user, info) {
        if (err) {
            return next(err);
        }
        if (!user) {
            return res.redirect('/login');
        }

        req.user = user;
        return next();
    })(req, res, next);
}

app.get('/login', (req, res) => {
    const resBody = fs.readFileSync(path.join(__dirname, 'views', 'login.html'));
    if (!resBody) {
        return res.status(500).json({ error: "Internal server error. Failed to load login page." });
    }
    res.header('Content-type','text/html;charset=utf-8');
    res.send(resBody);
});

app.post('/login', (req, res) => {
    console.log(req.body);
    const {login, password } = req.body;
    const user = findUser(login);
    if (!user || user.password != password) {
        return res.status(401).json({ error: "Unautorized. Invalid login or password" })
    }

    req.login({ login: user.login }, function (err) {
        if (err) {
            return next(err);
        }

        return res.redirect('/resource');
    });

});

app.get('/logout', (req, res) => {
    req.logout(function (err) {
        if (err) {
            return res.status(500).json({ error: "Internal server error. Failed to logout" });
        }

        req.session.destroy(() => {
            const resBody = fs.readFileSync(path.join(__dirname,'views','logout.html'));
            if(!resBody){
                return res.status(500).json({error:"Internal server error. Failed to load logout page"});
            }
            res.header('Content-type','text/html;charset=utf-8');
            res.send(resBody);
        });
    });
});

app.get('/resource', ensureAuthenticated, (req, res) => {
    const resBody = fs.readFileSync(path.join(__dirname,'views','resource.html'));
    if(!resBody){
        return res.status(500).json({error:"Internal server error. Failed to load resource page"});
    }
    
    res.header('Content-type','text/html');
    res.send(resBody);
});


app.use((req, res) => {
    res.status(404).json({ error: "Not found. Resource with corresponding URI could not be found" });
});

app.listen(PORT, () => {
    console.log(`Server listening at http://localhost:${PORT}/login`);
}); 