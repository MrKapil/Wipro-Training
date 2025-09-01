using System;

namespace UserManagement.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message, Exception? inner = null)
            : base(message, inner) { }
    }
}
