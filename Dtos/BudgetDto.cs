using System.ComponentModel.DataAnnotations;

namespace Frugal.Dtos
{
    /// <summary>
    /// A class that represents a user's budget.
    /// </summary>
    public class BudgetDto
    {
        /// <summary>
        /// Constructs a budget with a provided display name.
        /// </summary>
        /// <param name="name">The user-friendly name of the budget. Can't be empty.</param>
        public BudgetDto(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The unique identifier of the budget.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user friendly name of the budget.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}