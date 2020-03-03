using System.ComponentModel.DataAnnotations;

namespace Frugal.Models
{
    /// <summary>
    /// Represents a category within the database
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The unique id of the category within the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user-friendly display name of the category.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;
    }
}