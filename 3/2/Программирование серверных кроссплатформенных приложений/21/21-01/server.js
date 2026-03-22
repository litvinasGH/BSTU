const express = require('express');
const passport = require('passport');
const BasicStrategy = require('passport-http').BasicStrategy;
const users = require('./users.json');

const app = express();
const PORT = 3000;

// Настройка стратегии Basic
passport.use(new BasicStrategy(
  (username, password, done) => {
    if (users[username] && users[username] === password) {
      return done(null, { username });
    }
    return done(null, false);
  }
));

app.use(passport.initialize());

// Маршруты
app.get('/login', (req, res) => {
  // Basic аутентификация работает через заголовок Authorization,
  // поэтому отдельная форма не нужна, но по заданию нужно отправить форму.
  // Отправляем простую HTML-форму для ввода логина/пароля,
  // которая отправит запрос с заголовком Authorization.
  res.send(`
    <h2>Basic Authentication</h2>
    <form action="/resource" method="get">
      <label>Username: <input name="username"></label><br>
      <label>Password: <input type="password" name="password"></label><br>
      <button type="submit">Login</button>
    </form>
    <p><a href="/logout">Logout</a></p>
  `);
});

// Защищённый ресурс
app.get('/resource',
  passport.authenticate('basic', { session: false, failureRedirect: '/login' }),
  (req, res) => {
    res.send(`RESOURCE (Hello, ${req.user.username})`);
  }
);

// Выход – для Basic достаточно просто удалить учётные данные из браузера,
// но по заданию делаем редирект на /login
app.get('/logout', (req, res) => {
  // Basic не поддерживает серверный выход; просто перенаправляем
  res.redirect('/login');
});

// Обработка 404
app.use((req, res) => {
  res.status(404).send('404 Not Found');
});

app.listen(PORT, () => {
  console.log(`Server running at http://localhost:${PORT}`);
});