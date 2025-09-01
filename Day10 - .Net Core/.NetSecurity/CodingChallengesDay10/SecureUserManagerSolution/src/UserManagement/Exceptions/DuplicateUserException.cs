using System;

namespace UserManagement.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string username)
            : base($"User '{username}' already exists.") { }
    }
}
