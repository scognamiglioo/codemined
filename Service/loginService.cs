using learncode.Models;
using learncode.Data;
using Microsoft.EntityFrameworkCore;


public class LoginService
{
    private readonly AppDbContext _context;

    public LoginService(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegisterUserAsync(string email, string password, string role)
{
    var newUser = new User
    {
        Email = email,
        Password = password,
        Role = role
    };

    _context.Users.Add(newUser);  // Adicionar novo usuário
    await _context.SaveChangesAsync();  // Salvar mudanças no banco de dados
}


    public async Task<User> GetUserByIdAsync(int userId)
    {
    return await _context.Users.FindAsync(userId);  
}


public async Task UpdateUserAsync(User user)
{
    var existingUser = await _context.Users.FindAsync(user.Id);
    if (existingUser != null)
    {
        
        existingUser.Email = user.Email;
        existingUser.Password = user.Password; // Lembre-se de criptografar a senha se necessário
        existingUser.Role = user.Role;

        await _context.SaveChangesAsync();

        if (existingUser.Id == 1)
        {
            throw new InvalidOperationException("Não é possível atualizar o usuário administrador");
        }
    }
}

public async Task<bool> AuthenticateUserAsync(string email, string password)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    return user != null;
}



    


}

