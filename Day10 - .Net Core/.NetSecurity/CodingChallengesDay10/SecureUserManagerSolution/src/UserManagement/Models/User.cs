namespace UserManagement.Models
{
    public class User
    {
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = ""; // PBKDF2$SHA256$iters$saltB64$hashB64
        public string? EncryptedDetails { get; set; }   // AES-encrypted JSON of UserDetails
    }
}
