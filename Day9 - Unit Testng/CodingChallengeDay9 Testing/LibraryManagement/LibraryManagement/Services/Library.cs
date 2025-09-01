using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class Library
    {
        private readonly List<Book> _books = new();
        private readonly List<Borrower> _borrowers = new();

        public IReadOnlyList<Book> Books => _books.AsReadOnly();
        public IReadOnlyList<Borrower> Borrowers => _borrowers.AsReadOnly();

        // Adds a new book, returns true if added, false if ISBN already exists
        public bool AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (_books.Any(b => b.ISBN == book.ISBN)) return false;
            _books.Add(book);
            return true;
        }

        // Registers a borrower; false if card number already exists
        public bool RegisterBorrower(Borrower borrower)
        {
            if (borrower == null) throw new ArgumentNullException(nameof(borrower));
            if (_borrowers.Any(b => b.LibraryCardNumber == borrower.LibraryCardNumber)) return false;
            _borrowers.Add(borrower);
            return true;
        }

        // Borrow a book by ISBN & borrower card number
        public bool BorrowBook(string isbn, string libraryCardNumber)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null) return false;
            var borrower = _borrowers.FirstOrDefault(b => b.LibraryCardNumber == libraryCardNumber);
            if (borrower == null) return false;
            return borrower.BorrowBook(book);
        }

        // Return a book by ISBN & borrower card number
        public bool ReturnBook(string isbn, string libraryCardNumber)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null) return false;
            var borrower = _borrowers.FirstOrDefault(b => b.LibraryCardNumber == libraryCardNumber);
            if (borrower == null) return false;
            return borrower.ReturnBook(book);
        }

        public IEnumerable<Book> ViewBooks() => _books.ToList();

        public IEnumerable<Borrower> ViewBorrowers() => _borrowers.ToList();
    }
}
