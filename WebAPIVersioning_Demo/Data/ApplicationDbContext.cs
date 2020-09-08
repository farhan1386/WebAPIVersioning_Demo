using System.Data.Entity;
using WebAPIVersioning_Demo.Models;

namespace WebAPIVersioning_Demo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext():base("CustomerDB")
        {

        }
        public DbSet<CustomerV1> CustomerV1s { get; set; }
        public DbSet<CustomerV2> customerV2s { get; set; }
    }
}