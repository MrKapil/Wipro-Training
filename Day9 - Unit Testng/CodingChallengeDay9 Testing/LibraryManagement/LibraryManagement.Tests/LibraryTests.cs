using NUnit.Framework;
using LibraryManagement.Services;
using LibraryManagement.Models;
using System.Linq;

namespace LibraryManagement.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        private Library _library;

        [SetUp]
        public void SetUp()
        {
            _library = new Library();
        }

        [Test]
        public void AddBook_AddsBookSuccessfully()
        {
            var book = new Book("Title1", "Author1", "ISBN-1");
            var added = _library.AddBook(book);

            Assert.IsTrue(added);
            Assert.AreEqual(1, _library.Books.Count);
            Assert.AreEqual("Title1", _library.Books.First().Title);
        }

        [Test]
        public void RegisterBorrower_RegistersSuccessfully()
        {
            var borrower = new Borrower("John Doe", "CARD1");
            var registered = _library.RegisterBorrower(borrower);

            Assert.IsTrue(registered);
            Assert.AreEqual(1, _library.Borrowers.Count);
            Assert.AreEqual("John Doe", _library.Borrowers.First().Name);
        }

        [Test]
        public void BorrowBook_BookMarkedBorrowedAndAssociated()
        {
            var book = new Book("T", "A", "ISBN-10");
            _library.AddBook(book);

            var borrower = new Borrower("Alice", "CARD-10");
            _library.RegisterBorrower(borrower);

            var result = _library.BorrowBook("ISBN-10", "CARD-10");

            Assert.IsTrue(result);
            Assert.IsTrue(book.IsBorrowed);
            Assert.IsTrue(borrower.BorrowedBooks.Contains(book));
        }

        [Test]
        public void ReturnBook_BookMarkedAvailableAndRemovedFromBorrower()
        {
            var book = new Book("T2", "A2", "ISBN-20");
            _library.AddBook(book);

            var borrower = new Borrower("Bob", "CARD-20");
            _library.RegisterBorrower(borrower);

            _library.BorrowBook("ISBN-20", "CARD-20");
            var returned = _library.ReturnBook("ISBN-20", "CARD-20");

            Assert.IsTrue(returned);
            Assert.IsFalse(book.IsBorrowed);
            Assert.IsFalse(borrower.BorrowedBooks.Contains(book));
        }

        [Test]
        public void ViewBooksAndBorrowers_ReturnsLists()
        {
            _library.AddBook(new Book("T1", "A1", "ISBN-A"));
            _library.AddBook(new Book("T2", "A2", "ISBN-B"));
            _library.RegisterBorrower(new Borrower("Zed", "CARD-Z"));

            var books = _library.ViewBooks().ToList();
            var borrowers = _library.ViewBorrowers().ToList();

            Assert.AreEqual(2, books.Count);
            Assert.AreEqual(1, borrowers.Count);
        }
    }
}
