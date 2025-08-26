using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using todo_app.Models;

namespace todo_app.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base DbContext class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoItem> Todos => Set<TodoItem>(); 
    }
}
