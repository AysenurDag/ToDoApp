using Microsoft.EntityFrameworkCore;
using todo_app.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("AllowLocal", p => p
    .WithOrigins("https://localhost:5248", "http://localhost:5173", "https://localhost:{HTTPS_PORT}")
    .AllowAnyHeader()
    .AllowAnyMethod()
));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      
    app.UseSwaggerUI();    
}
app.UseCors("AllowLocal");

app.UseHttpsRedirection(); 
app.UseAuthorization();    
app.MapControllers();      

app.Run();
