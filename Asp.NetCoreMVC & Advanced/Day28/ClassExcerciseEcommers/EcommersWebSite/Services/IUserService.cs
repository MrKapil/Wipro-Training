namespace EcommerceApp.Services
{
    public interface IUserService
    {
        bool Validate(string username, string password);
    }
}
