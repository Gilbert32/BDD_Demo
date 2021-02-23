using BDD_Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BDD_Demo.Domain.Contexts
{
    public class InMemoryBddDemoContext: BddDemoContext
    {
        public InMemoryBddDemoContext()
        {
        }

        public InMemoryBddDemoContext(DbContextOptionsBuilder  options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("bdd_demo");
            }
        }
    }
}
