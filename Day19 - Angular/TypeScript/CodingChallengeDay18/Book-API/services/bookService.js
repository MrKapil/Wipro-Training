const fs = require("fs").promises;
const path = require("path");
const EventEmitter = require("events");

const filePath = path.join(__dirname, "../data/books.json");

class BookEmitter extends EventEmitter {}
const bookEmitter = new BookEmitter();

bookEmitter.on("bookAdded", () => console.log("📚 Book Added"));
bookEmitter.on("bookUpdated", () => console.log("✏️ Book Updated"));
bookEmitter.on("bookDeleted", () => console.log("❌ Book Deleted"));

async function readBooks() {
  try {
    const data = await fs.readFile(filePath, "utf-8");
    return JSON.parse(data);
  } catch (err) {
    console.error("Error reading books:", err);
    return [];
  }
}

async function writeBooks(books) {
  try {
    await fs.writeFile(filePath, JSON.stringify(books, null, 2));
  } catch (err) {
    console.error("Error writing books:", err);
  }
}

async function getAllBooks() {
  return await readBooks();
}

async function getBookById(id) {
  const books = await readBooks();
  return books.find((b) => b.id === id);
}

async function addBook(book) {
  const books = await readBooks();
  book.id = Date.now().toString();
  books.push(book);
  await writeBooks(books);
  bookEmitter.emit("bookAdded");
  return book;
}

async function updateBook(id, updatedData) {
  const books = await readBooks();
  const index = books.findIndex((b) => b.id === id);
  if (index === -1) return null;
  books[index] = { ...books[index], ...updatedData };
  await writeBooks(books);
  bookEmitter.emit("bookUpdated");
  return books[index];
}

async function deleteBook(id) {
  let books = await readBooks();
  const filtered = books.filter((b) => b.id !== id);
  if (filtered.length === books.length) return false;
  await writeBooks(filtered);
  bookEmitter.emit("bookDeleted");
  return true;
}

module.exports = {
  getAllBooks,
  getBookById,
  addBook,
  updateBook,
  deleteBook,
};
