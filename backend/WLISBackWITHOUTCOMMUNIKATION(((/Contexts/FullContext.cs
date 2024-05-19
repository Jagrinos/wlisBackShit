using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WLISBackend.models;
using WLISBackWITHOUTCOMMUNIKATION___.Auntefication;

namespace WLISBackWITHOUTCOMMUNIKATION___.Contexts
{
    public class FullContext :DbContext
    {
        public FullContext(DbContextOptions dbContext) : base(dbContext)
        {
            Database.EnsureCreated();
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; } 
        public DbSet<Merch> Merches { get; set; } 
        public DbSet<Message> Messages { get; set; } 
        public DbSet<Project> Projects { get; set; } 
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
