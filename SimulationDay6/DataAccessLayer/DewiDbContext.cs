using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimulationDay6.Models;

namespace SimulationDay6.DataAccessLayer
{
    public class DewiDbContext:IdentityDbContext<User>
    {
        public DewiDbContext(DbContextOptions opt):base(opt)
        {
            
        }
        public DbSet<Person>Persons { get; set; }
        public DbSet<Position>Positions { get; set; }
    }
}
