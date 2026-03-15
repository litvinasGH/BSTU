const express = require('express');
const fs = require('fs');

const app = express();
const PORT = 3000;
const DATA_FILE = path.join(__dirname, 'phonebook.json');


app.use(express.json());
app.use(express.urlencoded({ extended: true }));



async function readPhonebook() {
  try {
    const data = await fs.readFile(DATA_FILE, 'utf8');
    return JSON.parse(data);
  } catch (error) {
    console.error('Ошибка чтения файла:', error);
    return { contacts: [] };
  }
}

async function writePhonebook(data) {
  try {
    await fs.writeFile(DATA_FILE, JSON.stringify(data, null, 2), 'utf8');
    return true;
  } catch (error) {
    console.error('Ошибка записи в файл:', error);
    return false;
  }
}



app.get('/api/contacts', async (req, res) => {
  const phonebook = await readPhonebook();
  res.json(phonebook);
});

app.get('/api/contacts/:id', async (req, res) => {
  const phonebook = await readPhonebook();
  const contact = phonebook.contacts.find(c => c.id === parseInt(req.params.id));
  
  if (contact) {
    res.json(contact);
  } else {
    res.status(404).json({ error: 'Контакт не найден' });
  }
});

app.post('/api/contacts', async (req, res) => {
  const phonebook = await readPhonebook();
  
  const newContact = {
    id: phonebook.contacts.length > 0 ? Math.max(...phonebook.contacts.map(c => c.id)) + 1 : 1,
    name: req.body.name,
    phone: req.body.phone,
  };
  
  phonebook.contacts.push(newContact);
  
  if (await writePhonebook(phonebook)) {
    res.status(201).json(newContact);
  } else {
    res.status(500).json({ error: 'Ошибка при сохранении контакта' });
  }
});

app.put('/api/contacts/:id', async (req, res) => {
  const phonebook = await readPhonebook();
  const index = phonebook.contacts.findIndex(c => c.id === parseInt(req.params.id));
  
  if (index === -1) {
    return res.status(404).json({ error: 'Контакт не найден' });
  }
  
  phonebook.contacts[index] = {
    ...phonebook.contacts[index],
    ...req.body,
    id: parseInt(req.params.id)
  };
  
  if (await writePhonebook(phonebook)) {
    res.json(phonebook.contacts[index]);
  } else {
    res.status(500).json({ error: 'Ошибка при обновлении контакта' });
  }
});

app.delete('/api/contacts/:id', async (req, res) => {
  const phonebook = await readPhonebook();
  const initialLength = phonebook.contacts.length;
  
  phonebook.contacts = phonebook.contacts.filter(c => c.id !== parseInt(req.params.id));
  
  if (phonebook.contacts.length === initialLength) {
    return res.status(404).json({ error: 'Контакт не найден' });
  }
  
  if (await writePhonebook(phonebook)) {
    res.json({ message: 'Контакт успешно удален' });
  } else {
    res.status(500).json({ error: 'Ошибка при удалении контакта' });
  }
});

// HTML формы для работы со справочником
app.post('/add', async (req, res) => {
  const phonebook = await readPhonebook();
  
  const newContact = {
    id: phonebook.contacts.length > 0 ? Math.max(...phonebook.contacts.map(c => c.id)) + 1 : 1,
    name: req.body.name,
    phone: req.body.phone,
    email: req.body.email
  };
  
  phonebook.contacts.push(newContact);
  await writePhonebook(phonebook);
  
  res.redirect('/');
});

app.post('/delete/:id', async (req, res) => {
  const phonebook = await readPhonebook();
  phonebook.contacts = phonebook.contacts.filter(c => c.id !== parseInt(req.params.id));
  await writePhonebook(phonebook);
  res.redirect('/');
})











app.listen(PORT, () => {
  console.log(`Сервер 20-01 запущен и слушает порт ${PORT}`);
  console.log(`Откройте http://localhost:${PORT} в браузере`);
});