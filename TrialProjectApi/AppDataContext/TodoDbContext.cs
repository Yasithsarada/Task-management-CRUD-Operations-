using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using TrialProjectApi.Model;

namespace TrialProjectApi.AppDataContext
{
    public class TodoDbContext: DbContext
    {
        private readonly DBSettings _dBSettings;

       public TodoDbContext( IOptions<DBSettings> dbSettings)
        {
            _dBSettings = dbSettings.Value;
        }
        public DbSet<TodoModel> Todos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoModel>().ToTable("TodoAPI").HasKey(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_dBSettings.ConnectionString);
            //optionsBuilder.UseSqlServer(_dBSettings.ConnectionString);
            
                //db.Database.EnsureCreated(); // Creates database and tables if they don't exist
            
            //optionsBuilder.UseSqlite(_dBSettings.ConnectionString);
            optionsBuilder.UseSqlite("Data Source=mydatabase.db");
        }
    }
}
