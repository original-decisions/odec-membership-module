using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace User.DAL.Tests
{
    public class Tester<TDbContext> 
        where TDbContext : DbContext
    {
        protected Tester()
        {
        }
        [OneTimeSetUp]
        public virtual void Init()
        {
            //var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

            //optionsBuilder.UseInMemoryDatabase();

            Options= CreateNewContextOptions();
        }

        public DbContextOptions<TDbContext> Options { get; set; }

        public static DbContextOptions<TDbContext> CreateNewContextOptions(IServiceCollection services=null)
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            if (services==null)
            {
                services = new ServiceCollection();
            }
            var serviceProvider = services
                  .AddEntityFrameworkInMemoryDatabase()
                  .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<TDbContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }


    }
}
