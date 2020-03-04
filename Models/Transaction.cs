using System;
using System.ComponentModel.DataAnnotations;

namespace Frugal.Models
{
    /// <summary>
    /// Represents a transaction within the database.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Creates a transaction with a provided category and business.
        /// </summary>
        /// <param name="category">The category to which transaction belongs.</param>
        /// <param name="business">The business which was involved in the transaction.</param>
        public Transaction(Category category, Business business)
        {
            Category = category;
            Business = business;
        }

        /// <summary>
        /// The unique id of the transaction.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The amount of the transaction
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// The time at which the transaction ocurred.
        /// </summary>
        [Required]
        public DateTime Time { get; set; }

        /// <summary>
        /// The category to which the transaction belongs.
        /// </summary>
        [Required]
        public Category Category { get; set; }

        /// <summary>
        /// The business which was involved in the transaction.
        /// </summary>
        [Required]
        public Business Business { get; set; }

        /// <summary>
        /// The optional description providing additional details about the transaction.
        /// </summary>
        public string? Description { get; set; }
    }
}