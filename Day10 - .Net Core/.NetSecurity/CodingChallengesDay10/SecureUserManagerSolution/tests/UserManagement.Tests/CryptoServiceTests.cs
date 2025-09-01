using System.Security.Cryptography;
using UserManagement.Services;
using Xunit;

public class CryptoServiceTests
{
    private static CryptoService NewCrypto() => new CryptoService(RandomNumberGenerator.GetBytes(32));

    [Fact]
    public void Hash_Then_Verify_Password_Works()
    {
        var crypto = NewCrypto();
        var hash = crypto.HashPassword("P@ssw0rd!");
        Assert.True(crypto.VerifyPassword("P@ssw0rd!", hash));
        Assert.False(crypto.VerifyPassword("wrong", hash));
    }

    [Fact]
    public void Encrypt_Then_Decrypt_Roundtrip()
    {
        var crypto = NewCrypto();
        var text = "Sensitive data 123";
        var enc = crypto.Encrypt(text);
        var dec = crypto.Decrypt(enc);
        Assert.Equal(text, dec);
    }
}
