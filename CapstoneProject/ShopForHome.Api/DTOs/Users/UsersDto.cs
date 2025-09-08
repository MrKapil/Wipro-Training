namespace ShopForHome.Api.DTOs.Users
{
    public class UserDto
    {
        public long UserId { get; set; }    // keep long to match existing User entity if it's long
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
    }

    public class UserCreateDto
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "User";
    }

    public class UserUpdateDto
    {
        public string FullName { get; set; } = "";
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
    }
}
