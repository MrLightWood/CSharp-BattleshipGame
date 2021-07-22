using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.Extensions.Options;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;
        //public DbSet<PlayerShip> PlayerShips { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Bot> Bots { get; set; } = null!;
        public DbSet<Ship> Ships { get; set; } = null!;
        public DbSet<BoardState> BoardStates { get; set; } = null!;

        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                //.UseLoggerFactory(_loggerFactory)
                //.EnableSensitiveDataLogging()
                .UseSqlServer(@"
                    Server=SAILEKEYEV; 
                    Database=csharp;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true"
                );
        }
        */
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
            
            /*
            modelBuilder
                .Entity<Game>()
                .HasMany<Player>()
                .WithOne(p=>p.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            */
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        
        }
    }
}

/*
private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(
    builder =>
    {
        builder
            .AddFilter("Microsoft", LogLevel.Information)
            .AddConsole();
    }
);

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
    optionsBuilder
        .UseLoggerFactory(_loggerFactory)
        .EnableSensitiveDataLogging()
        .UseSqlServer(@"
            Server=SAILEKEYEV; 
            Database=csharp;
            Trusted_Connection=True;
            MultipleActiveResultSets=true"
        );
    //.UseSqlite("Data Source=C:\\Users\\saile\\RiderProjects\\DbDemo\\ConsoleApp\\app.db");
}
*/