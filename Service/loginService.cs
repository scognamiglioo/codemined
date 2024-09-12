using codemined.Models;
using codemined.Data;

public class LoginService
{
    private readonly ApplicationDbContext _context;

    public LoginService()
    {
        _context = new ApplicationDbContext();
    }

    public bool RegisterUser(string username, string password)
    {
        var user = new User
        {
            Username = username,
            Password = password,
            Role = "User"
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return true;
    }
}
