using learncode.Models;
using learncode.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

public class LoginService
{
    private readonly AppDbContext _context;

    public LoginService(AppDbContext context)
    {
        _context = context;
    }

    // Create - Registrar usuário
    public async Task RegisterUserAsync(string email, string password, string role)
{
    Console.WriteLine($"Email recebido: {email}");
    Console.WriteLine($"Senha recebida: {password}");
    Console.WriteLine($"Role recebido: {role}");

    var newUser = new User
    {
        Email = email,
        Password = password,
        Role = role
    };

    _context.Users.Add(newUser);
    await _context.SaveChangesAsync();
}


    // Read - Obter usuário por ID
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    // Read - Obter todos os usuários
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Update - Atualizar usuário
    public async Task UpdateUserAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser != null)
        {
            existingUser.Email = user.Email;
            existingUser.Password = user.Password; 
            existingUser.Role = user.Role;

            await _context.SaveChangesAsync();

            if (existingUser.Id == 1)
            {
                throw new InvalidOperationException("Não é possível atualizar o usuário administrador");
            }
        }
    }

    // Delete - Excluir usuário
    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null && user.Id != 1) // Evita deletar o administrador
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Não é possível excluir o usuário administrador");
        }
    }

    // Autenticação
    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        return user != null;
    }
}
