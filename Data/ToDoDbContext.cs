using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics;
using ToDoManagerAPI.Models;

namespace ToDoManagerAPI.Data
{
    public class ToDoDbContext: DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }
        //protected readonly IConfiguration Configuration = configuration;

        // Manual configuration the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            //options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            // Print the computed path so you can verify it at runtime
            Console.WriteLine($"ToDo database path: {path}");
            Debug.WriteLine($"ToDo database path: {path}");

            options.UseSqlite($"Data Source={Path.Join(path, "LocalDatabase.db")}");
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}


