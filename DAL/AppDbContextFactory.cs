using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(
                @"
                    Server=SAILEKEYEV; 
                    Database=csharp;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true"
            );
            
            return new AppDbContext(optionsBuilder.Options);
            //return new AppDbContext();
        }
    }
}