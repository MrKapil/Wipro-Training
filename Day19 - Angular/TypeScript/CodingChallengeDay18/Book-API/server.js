const express = require("express");
const bookService = require("./services/bookService");

const app = express();
app.use(express.json());

app.get("/", (req, res) => {
  res.json({ message: "Welcome to Book Management API" });
});

// Get all books
app.get("/books", async (req, res) => {
  const books = await bookService.getAllBooks();
  res.json(books);
});

// Get book by ID
app.get("/books/:id", async (req, res) => {
  const book = await bookService.getBookById(req.params.id);
  if (!book) return res.status(404).json({ message: "Book not found" });
  res.json(book);
});

// Add a new book
app.post("/books", async (req, res) => {
  const { title, author } = req.body;
  if (!title || !author) {
    return res.status(400).json({ message: "Title and Author are required" });
  }
  const newBook = await bookService.addBook({ title, author });
  res.status(201).json(newBook);
});

// Update book
app.put("/books/:id", async (req, res) => {
  const updatedBook = await bookService.updateBook(req.params.id, req.body);
  if (!updatedBook) return res.status(404).json({ message: "Book not found" });
  res.json(updatedBook);
});

// Delete book
app.delete("/books/:id", async (req, res) => {
  const success = await bookService.deleteBook(req.params.id);
  if (!success) return res.status(404).json({ message: "Book not found" });
  res.json({ message: "Book deleted" });
});

const PORT = 3000;
app.listen(PORT, () => console.log(`🚀 Server running on port ${PORT}`));
