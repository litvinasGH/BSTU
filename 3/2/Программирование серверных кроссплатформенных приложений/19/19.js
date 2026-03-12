const express = require('express');
const userRoutes = require('./routes/userRoutes');
const orderRoutes = require('./routes/orderRoutes');
const bookRoutes = require('./routes/bookRoutes');

const app = express();
const PORT = 3000;

app.use(express.json());

app.use('/users/', userRoutes);
app.use('/orders/', orderRoutes);
app.use('/books/', bookRoutes);

app.listen(PORT, () => {
    console.log(`Server running at http://localhost:${PORT}`);
});