using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace PetAdoption.Models
{
    public class PetAdoptionContextDB:IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Customer> Customers {get; set;}
        public DbSet<Pet> Pets {get;set;}
        public DbSet<CustomerPet> CustomerPets {get;set;}
        public PetAdoptionContextDB(DbContextOptions options):base(options)
        {
            
            
        }
        // protected  override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //         modelBuilder.Entity<CustomerPet>().HasKey(x=>new{x.CustomerId,x.PetId});
        // }

    }
}