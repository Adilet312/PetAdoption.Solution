using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PetAdoption.Models
{
  public class PetAdoptionContextFactory : IDesignTimeDbContextFactory<PetAdoptionContextDB>
  {

    PetAdoptionContextDB IDesignTimeDbContextFactory<PetAdoptionContextDB>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<PetAdoptionContextDB>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new PetAdoptionContextDB(builder.Options);
    }
  }
}