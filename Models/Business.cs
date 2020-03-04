using System.ComponentModel.DataAnnotations;

namespace Frugal.Models
{
    /// <summary>
    /// Represents a business within the database.
    /// </summary>
    public class Business
    {
        /// <summary>
        /// The unique id of the business within the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user-friendly display name of the business.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;
    }
}