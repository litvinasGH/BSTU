const express = require("express");
const fs = require("fs");
const path = require("path");
const exphbs = require("express-handlebars");

const app = express();
const PORT = 3000;

app.use(express.urlencoded({ extended: true }));
app.use(express.static("public"));

app.engine("hbs", exphbs.engine({
    extname: "hbs",
    layoutsDir: "views/layouts",
    partialsDir: "views/partials",
    defaultLayout: "main",
    helpers: {
        cancel: function () {
            return '<a href="/">Отказаться</a>';
        }
    }
}));

app.set("view engine", "hbs");
app.set("views", "./views");

const file = path.join(__dirname, "phonebook.json");

function readData() {
    return JSON.parse(fs.readFileSync(file));
}

function writeData(data) {
    fs.writeFileSync(file, JSON.stringify(data, null, 2));
}

app.get("/", (req, res) => {
    const data = readData();
    res.render("index", { contacts: data });
});

app.get("/Add", (req, res) => {
    const data = readData();
    res.render("add", { contacts: data });
});

app.post("/Add", (req, res) => {
    const data = readData();

    const newContact = {
        id: Date.now(),
        name: req.body.name,
        phone: req.body.phone
    };

    data.push(newContact);
    writeData(data);

    res.redirect("/");
});

app.get("/Update/:id", (req, res) => {
    const data = readData();
    const contact = data.find(x => x.id == req.params.id);

    res.render("update", {
        contacts: data,
        contact
    });
});

app.post("/Update", (req, res) => {
    let data = readData();

    data = data.map(c => {
        if (c.id == req.body.id) {
            c.name = req.body.name;
            c.phone = req.body.phone;
        }
        return c;
    });

    writeData(data);
    res.redirect("/");
});

app.post("/Delete", (req, res) => {
    let data = readData();

    data = data.filter(c => c.id != req.body.id);

    writeData(data);
    res.redirect("/");
});

app.listen(PORT, () => {
    console.log("Server started on port 3000");
});