using Serilog;
using UserManagement.Models;
using UserManagement.Repos;
using UserManagement.Services;

namespace UserManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog: log to rolling file
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                var crypto = CryptoService.FromEnvironmentOrRandom();
                if (Environment.GetEnvironmentVariable("APP_AES_KEY") is null)
                {
                    Log.Warning("APP_AES_KEY not set. Using a randomly generated key for this session.");
                }

                var repo = new InMemoryUserRepository();
                var svc = new UserService(repo, crypto);

                Console.WriteLine("=== Simple User Management Demo ===");
                Console.Write("Register username: ");
                var u = Console.ReadLine() ?? "";
                Console.Write("Register password: ");
                var p = ReadPassword();

                Console.Write("Full name: ");
                var name = Console.ReadLine() ?? "";
                Console.Write("Email: ");
                var email = Console.ReadLine() ?? "";

                svc.Register(u, p, new UserDetails { FullName = name, Email = email });
                Console.WriteLine("Registered successfully.\n");

                Console.Write("Login username: ");
                var lu = Console.ReadLine() ?? "";
                Console.Write("Login password: ");
                var lp = ReadPassword();

                var ok = svc.Authenticate(lu, lp);
                Console.WriteLine(ok ? "Login success." : "Login failed.");

                if (ok)
                {
                    var details = svc.GetDecryptedDetails(lu);
                    if (details != null)
                        Console.WriteLine($"Decrypted details -> Name: {details.FullName}, Email: {details.Email}");
                }

                Console.WriteLine("\nCheck 'logs' folder for log files.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static string ReadPassword()
        {
            var chars = new Stack<char>();
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && chars.Count > 0)
                {
                    chars.Pop();
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    chars.Push(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return new string(chars.Reverse().ToArray());
        }
    }
}

