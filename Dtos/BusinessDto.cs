using System.ComponentModel.DataAnnotations;

namespace Frugal.Dtos
{
    /// <summary>
    /// A class that represents a budget to which a transaction was made.
    /// </summary>
    public class BusinessDto
    {
        /// <summary>
        /// The unique id of the business in the database.
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