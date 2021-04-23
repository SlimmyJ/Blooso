using Blooso.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Xamarin.Essentials;

namespace Blooso.Data
{
    public sealed class BloosoContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLocation> UserLocations { get; set; }

        public BloosoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map relations
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);

            modelBuilder.Entity<User>().HasMany(x => x.Tags);
            modelBuilder.Entity<Tag>().HasMany(x => x.Users);
            modelBuilder.Entity<Activity>().HasMany(x => x.Users);
            modelBuilder.Entity<User>().HasMany(x => x.Activities);

            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);

            // Seed Data
            modelBuilder.Entity<Tag>().HasData(
                new Tag(1, "Outdoors"),
                new Tag(2, "Talking"),
                new Tag(3, "Wine"),
                new Tag(4, "Clubbing"),
                new Tag(5, "Travel"),
                new Tag(6, "Movies"),
                new Tag(7, "Music"),
                new Tag(8, "Larping"),
                new Tag(9, "Gaming"),
                new Tag(10, "Inked"),
                new Tag(11, "Photography"),
                new Tag(12, "Arts"),
                new Tag(13, "Polyamory"),
                new Tag(14, "Cooking"),
                new Tag(15, "Books"),
                new Tag(16, "Conspiracies"),
                new Tag(17, "FlatEarther"),
                new Tag(18, "Gambling"),
                new Tag(19, "Religion"),
                new Tag(20, "Pets"),
                new Tag(21, "Smoker"),
                new Tag(22, "Trekkie"),
                new Tag(23, "Furry"),
                new Tag(24, "Weeb"),
                new Tag(25, "Festivals"));

            modelBuilder.Entity<Activity>().HasData(
                new Activity(1, "Running"),
                new Activity(2, "Walking"),
                new Activity(3, "Tennis"),
                new Activity(4, "Golf"),
                new Activity(5, "Padel"),
                new Activity(6, "Minigolf"),
                new Activity(7, "Camping"),
                new Activity(8, "Basketball"),
                new Activity(9, "Cycling"),
                new Activity(10, "Handball"),
                new Activity(11, "Climbing"),
                new Activity(12, "Squash"),
                new Activity(13, "Fitness"),
                new Activity(14, "Sauna"),
                new Activity(15, "Lacrosse"),
                new Activity(16, "Polo"),
                new Activity(17, "Football"),
                new Activity(18, "Yoga"),
                new Activity(19, "Dancing"),
                new Activity(20, "Pilates"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }
    }
}