using learncode.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using learncode.Data;
using learncode.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar o contexto de banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=learncode.db"));



// Registrar o LoginService
builder.Services.AddScoped<LoginService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Verificar se o banco de dados está vazio
    if (!context.Users.Any())
    {
        // Adicionar usuários de teste
        context.Users.AddRange(
            new User { Email = "admin@example.com", Password = "admin", Role = "Admin" },
            new User { Email = "user@example.com", Password = "user", Role = "User" }

        );
        context.SaveChanges();
    }
}


// Configurar o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();




