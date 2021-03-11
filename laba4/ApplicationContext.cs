using System.Data.Entity;

namespace laba4
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }

        public DbSet<Debt> Debts { get; set; }
    }
}