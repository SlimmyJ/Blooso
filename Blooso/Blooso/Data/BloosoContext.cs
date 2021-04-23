using System.IO;
using Blooso.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace Blooso.Data
{
    public sealed class BloosoContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLocation> UserLocations { get; set; }

        public BloosoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);
            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }
    }
}