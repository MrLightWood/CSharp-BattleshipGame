using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain;

namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
    /*
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Game> Games { get; set; } = null!;
        //public DbSet<PlayerShip> PlayerShips { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Ship> Ships { get; set; } = null!;
        public DbSet<BoardState> BoardStates { get; set; } = null!;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Player>()
                .HasOne<Game>()
                .WithOne(x => x.PlayerA)
                .HasForeignKey<Game>(x => x.PlayerAId);
            
            modelBuilder
                .Entity<Player>()
                .HasOne<Game>()
                .WithOne(x => x.PlayerB)
                .HasForeignKey<Game>(x => x.PlayerBId);
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        
        }
    }
    */
}