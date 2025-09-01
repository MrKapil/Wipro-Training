using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Borrower
    {
        public string Name { get; }
        public string LibraryCardNumber { get; }
        private readonly List<Book> _borrowedBooks = new();
        public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks.AsReadOnly();

        public Borrower(string name, string libraryCardNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LibraryCardNumber = libraryCardNumber ?? throw new ArgumentNullException(nameof(libraryCardNumber));
        }

        // Attempt to borrow a book. Returns true on success.
        public bool BorrowBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (book.IsBorrowed) return false;
            var borrowed = book.Borrow();
            if (borrowed)
            {
                _borrowedBooks.Add(book);
                return true;
            }
            return false;
        }

        // Attempt to return a book. Returns true on success.
        public bool ReturnBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (!_borrowedBooks.Contains(book)) return false;
            var returned = book.Return();
            if (returned)
            {
                _borrowedBooks.Remove(book);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            var count = _borrowedBooks.Count;
            return $"{Name} (Card: {LibraryCardNumber}) - Borrowed books: {count}";
        }
    }
}