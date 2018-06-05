using Microsoft.EntityFrameworkCore;

namespace DaData.Models
{
    public class DaDataContext: DbContext
    {
        public DaDataContext(DbContextOptions<DaDataContext> options)
            : base(options)
        {
        }

        public DbSet<user> users {get; set;} 
        public DbSet<city> as_addrobj{get;set;}
    }
}