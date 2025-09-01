using System.Text.Json;
using Serilog;
using UserManagement.Exceptions;
using UserManagement.Models;
using UserManagement.Repos;

namespace UserManagement.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly CryptoService _crypto;

        public UserService(IUserRepository repo, CryptoService crypto)
        {
            _repo = repo;
            _crypto = crypto;
        }

        public User Register(string username, string password, UserDetails details)
        {
            try
            {
                var existing = _repo.GetByUsername(username);
                if (existing is not null)
                    throw new DuplicateUserException(username);

                var user = new User
                {
                    Username = username,
                    PasswordHash = _crypto.HashPassword(password),
                    EncryptedDetails = _crypto.Encrypt(JsonSerializer.Serialize(details))
                };

                _repo.Add(user);
                Log.Information("User '{Username}' registered successfully at {Timestamp}.", username, DateTimeOffset.UtcNow);
                return user;
            }
            catch (DuplicateUserException)
            {
                // expected validation failure; log minimal info
                Log.Warning("Attempt to register duplicate user '{Username}' at {Timestamp}.", username, DateTimeOffset.UtcNow);
                throw; // keep message generic up the stack if shown to user
            }
            catch (Exception ex)
            {
                // No sensitive info in logs
                Log.Error(ex, "Unexpected error during registration for '{Username}'.", username);
                throw new AppException("Registration failed due to an unexpected error.", ex);
            }
        }

        public bool Authenticate(string username, string password)
        {
            try
            {
                var user = _repo.GetByUsername(username);
                if (user is null)
                {
                    Log.Information("Login failed: user '{Username}' not found.", username);
                    return false;
                }

                var ok = _crypto.VerifyPassword(password, user.PasswordHash);
                Log.Information("Login {Result} for '{Username}' at {Timestamp}.",
                                ok ? "success" : "failed", username, DateTimeOffset.UtcNow);
                return ok;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error during login for '{Username}'.", username);
                throw new AppException("Login failed due to an unexpected error.", ex);
            }
        }

        public UserDetails? GetDecryptedDetails(string username)
        {
            try
            {
                var user = _repo.GetByUsername(username);
                if (user?.EncryptedDetails is null) return null;

                var json = _crypto.Decrypt(user.EncryptedDetails);
                return JsonSerializer.Deserialize<UserDetails>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error decrypting details for '{Username}'.", username);
                throw new AppException("Could not decrypt user details.", ex);
            }
        }
    }
}
