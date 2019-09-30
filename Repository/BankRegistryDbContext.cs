using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class BankRegistryDbContext : DbContext
    {
        public BankRegistryDbContext(DbContextOptions<BankRegistryDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
