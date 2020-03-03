using Frugal.Models;
using Microsoft.EntityFrameworkCore;

namespace Frugal.Data
{
    /// <summary>
    /// The database context used by the Frugal application
    /// </summary>
    public class FrugalContext : DbContext
    {
        /// <summary>
        /// Constructs a database context with the supplied options.
        /// </summary>
        /// <param name="options">The options used to configure the context.</param>
        public FrugalContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// The database set of Budget objects within the database.
        /// </summary>
        public DbSet<Budget> Budgets { get; set; } = null!;

        /// <summary>
        /// The database set of Category objects within the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; } = null!;
    }
}