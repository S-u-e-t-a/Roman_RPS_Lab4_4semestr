using System.Data.Entity;

namespace laba4
{
    /// <summary>
    /// Класс описывающий базу данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }

        public DbSet<Debt> Debts { get; set; }
    }
}