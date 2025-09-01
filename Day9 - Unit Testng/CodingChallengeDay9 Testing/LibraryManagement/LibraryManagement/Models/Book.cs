using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace LibraryManagement.Models
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string ISBN { get; }
        public bool IsBorrowed { get; private set; }

        public Book(string title, string author, string isbn)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
            IsBorrowed = false;
        }

        // Marks the book borrowed; returns true if success, false if already borrowed
        public bool Borrow()
        {
            if (IsBorrowed) return false;
            IsBorrowed = true;
            return true;
        }

        // Marks the book returned; true if success, false if it wasn't borrowed
        public bool Return()
        {
            if (!IsBorrowed) return false;
            IsBorrowed = false;
            return true;
        }

        public override string ToString()
            => $"{Title} by {Author} (ISBN: {ISBN}) - {(IsBorrowed ? "Borrowed" : "Available")}";
    }
}

