using System.ComponentModel.DataAnnotations;

namespace Frugal.Models
{
    /// <summary>
    /// Represents a user-defined budget in the database.
    /// </summary>
    public class Budget
    {
        /// <summary>
        /// Constructs a new budget with the provided user-friendly name.
        /// </summary>
        /// <param name="name">The user-friendly name to use for this budget. Can't be empty</param>
        public Budget(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The unique id of this budget in the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user-friendly display name of this budget.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}