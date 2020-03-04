using System;
using System.ComponentModel.DataAnnotations;

namespace Frugal.Dtos
{
    /// <summary>
    /// Represents a transaction and its associated metadata.
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Constructs a transaction with a provided category and business.
        /// </summary>
        /// <param name="category">The category to which this transaction belongs.</param>
        /// <param name="business">The business which was involved in this transaction.</param>
        public TransactionDto(CategoryDto category, BusinessDto business)
        {
            Category = category;
            Business = business;
        }

        /// <summary>
        /// The unique id of the transaction within the databse.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The time at which the transaction occurred.
        /// </summary>
        [Required]
        public DateTime Time { get; set; }

        /// <summary>
        /// The amount of money the transaction was for.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The category to which the transaction belongs.
        /// </summary>
        [Required]
        public CategoryDto Category { get; set; }

        /// <summary>
        /// The business with which the transaction ocurred.
        /// </summary>
        [Required]
        public BusinessDto Business { get; set; }

        /// <summary>
        /// An optional description providing additional details about the transaction.
        /// </summary>
        public string? Description { get; set; }
    }
}