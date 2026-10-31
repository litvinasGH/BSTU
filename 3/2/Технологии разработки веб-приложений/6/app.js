const express = require('express');
const sql = require('mssql');

const app = express();
app.use(express.json());

const config = {
  user: 'sa',
  password: 'StrongPass123!',
  server: 'mssql',
  port: 1433,
  database: 'Celebrities',
  options: {
    encrypt: false,
    trustServerCertificate: true
  }
};

// 🔥 Подключение с retry
async function getPool() {
  try {
    const pool = await sql.connect(config);
    console.log('Connected to MSSQL');
    return pool;
  } catch (err) {
    console.log('DB not ready, retrying in 3 sec...');
    await new Promise(res => setTimeout(res, 3000));
    return getPool();
  }
}

// 🔹 GET
app.get('/celebrities', async (req, res) => {
  try {
    const pool = await getPool();
    const result = await pool.request().query('SELECT * FROM Celebrity');
    res.json(result.recordset);
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// 🔹 POST
app.post('/celebrities', async (req, res) => {
  try {
    const { FullName, Profession } = req.body;
    const pool = await getPool();

    const result = await pool.request()
      .input('FullName', sql.NVarChar, FullName)
      .input('Profession', sql.NVarChar, Profession)
      .query(`
        INSERT INTO Celebrity (FullName, Profession)
        OUTPUT INSERTED.*
        VALUES (@FullName, @Profession)
      `);

    res.json(result.recordset[0]);
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// 🔹 PUT
app.put('/celebrities/:id', async (req, res) => {
  try {
    const { FullName, Profession } = req.body;
    const pool = await getPool();

    const result = await pool.request()
      .input('Id', sql.Int, req.params.id)
      .input('FullName', sql.NVarChar, FullName)
      .input('Profession', sql.NVarChar, Profession)
      .query(`
        UPDATE Celebrity
        SET FullName=@FullName, Profession=@Profession
        OUTPUT INSERTED.*
        WHERE Id=@Id
      `);

    res.json(result.recordset[0]);
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// 🔹 DELETE
app.delete('/celebrities/:id', async (req, res) => {
  try {
    const pool = await getPool();

    await pool.request()
      .input('Id', sql.Int, req.params.id)
      .query('DELETE FROM Celebrity WHERE Id=@Id');

    res.json({ deleted: true });
  } catch (err) {
    res.status(500).send(err.message);
  }
});

app.listen(3000, () => console.log('API started'));