using System.IO;
using Serilog;
using UserManagement.Exceptions;
using UserManagement.Models;
using UserManagement.Repos;
using UserManagement.Services;
using Xunit;

public class UserServiceTests
{
    private static (UserService svc, string logDir) SetupWithLogging()
    {
        var logDir = Path.Combine(Path.GetTempPath(), "um_logs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(logDir);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(logDir, "test-.log"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var svc = new UserService(new InMemoryUserRepository(), CryptoService.FromEnvironmentOrRandom());
        return (svc, logDir);
    }

    [Fact]
    public void Register_And_Login_Success()
    {
        var (svc, _) = SetupWithLogging();
        svc.Register("alice", "Secret#1", new UserDetails { FullName = "Alice", Email = "a@x.com" });
        Assert.True(svc.Authenticate("alice", "Secret#1"));
        Assert.False(svc.Authenticate("alice", "Wrong"));
        Assert.False(svc.Authenticate("nope", "whatever"));
    }

    [Fact]
    public void Duplicate_Registration_Throws_And_Logs()
    {
        var (svc, logDir) = SetupWithLogging();
        svc.Register("bob", "Pass!23", new UserDetails { FullName = "Bob", Email = "b@y.com" });

        Assert.Throws<DuplicateUserException>(() =>
            svc.Register("bob", "Other", new UserDetails { FullName = "Bobby", Email = "b2@y.com" })
        );

        Log.CloseAndFlush();

        var logFile = Directory.GetFiles(logDir)[0];
        var content = File.ReadAllText(logFile);
        Assert.Contains("duplicate", content, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Decrypt_Details_Works()
    {
        var (svc, _) = SetupWithLogging();
        svc.Register("cara", "Zxcv123!", new UserDetails { FullName = "Cara D", Email = "c@z.com" });
        var details = svc.GetDecryptedDetails("cara");
        Assert.NotNull(details);
        Assert.Equal("Cara D", details!.FullName);
        Assert.Equal("c@z.com", details.Email);
    }
}
