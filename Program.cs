using Microsoft.EntityFrameworkCore;
using todo_app.Data;

var builder = WebApplication.CreateBuilder(args);

// EF Core: ConnectionString'i okuyup SQL Server'a bağla
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// MVC Controller hizmetleri
builder.Services.AddControllers();

// Swagger (geliştirmede API keşfi ve test)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Program.cs
builder.Services.AddCors(o => o.AddPolicy("AllowLocal", p => p
    .WithOrigins("https://localhost:5248", "http://localhost:5173", "https://localhost:{HTTPS_PORT}")
    .AllowAnyHeader()
    .AllowAnyMethod()
));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // JSON
    app.UseSwaggerUI();    // Arayüz
}
app.UseCors("AllowLocal");

app.UseHttpsRedirection(); // HTTP -> HTTPS
app.UseAuthorization();    // (Şimdilik policy yok; ileride [Authorize] eklediğinde çalışır)
app.MapControllers();      // Controller route'larını aktif et

app.Run();
