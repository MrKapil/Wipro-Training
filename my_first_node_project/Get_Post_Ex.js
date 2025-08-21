const express = require('express');

const app = express();

app.get('/', (req, res) => {
res.send('Welcome to the homepage!');
});

app.get('/about', (req, res) => {
res.send('Learn more about us on this page.');
});

app.post('/contact', (req, res) => {
res.send('Thank you for reaching out!');
});

const port = 3000;
app.listen(port, () => {
console.log(`Server running on [URL]}`);
});