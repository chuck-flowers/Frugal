using System.ComponentModel.DataAnnotations;

namespace Frugal.Dtos
{
    /// <summary>
    /// A class that represents a category of transaction.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// The unique id of the category within the database.
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