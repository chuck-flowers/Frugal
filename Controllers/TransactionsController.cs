using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Frugal.Data;
using Frugal.Dtos;
using Frugal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Frugal.Controllers
{
    /// <summary>
    /// The controller responsible for managing the transaction entities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        /// <summary>
        /// The object that is used to interact with the database.
        /// </summary>
        private FrugalContext DbContext { get; }

        /// <summary>
        /// The object that is used to map between DTOs and database models.
        /// </summary>
        private IMapper Mapper { get; }

        /// <summary>
        /// Constructs a new TransactionsController
        /// </summary>
        /// <param name="dbContext">The FrugalContext with which to update the databse.</param>
        /// <param name="mapper">The mapper used to convert between DTOs and database models.</param>
        public TransactionsController(FrugalContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        /// <summary>
        /// Retrieves all the transactions
        /// </summary>
        /// <response code="200">Returns the list of transactions</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<TransactionDto> GetTransactionsAsync() => DbContext.Transactions.Select(Mapper.Map<Transaction, TransactionDto>);

        /// <summary>
        /// Retrieves the transaction matching an id.
        /// </summary>
        /// <param name="id">The id of the transaction to fetch.</param>
        /// <response code="200">Returns the matching transaction.</response>
        /// <response code="404">No transaction with the id was found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> GetTransactionByIdAsync(int id)
        {
            // Get the transaction using the provided id
            var transaction = await DbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
                return NotFound();

            // Return the DTO version of the transaction
            return Ok(Mapper.Map<TransactionDto>(transaction));
        }

        /// <summary>
        /// Updates a transaction within the database.
        /// </summary>
        /// <param name="transaction">The transaction to update within the database.</param>
        /// <response code="200">The transaction was successfully updated</response>
        /// <response code="404">No transaction with the provided ID could be found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTransactionAsync(TransactionDto transaction)
        {
            // Get the transaction using the supplied id
            var existingTransaction = await DbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
            if (existingTransaction == null)
                return NotFound();

            // Update the DB model
            Mapper.Map(transaction, existingTransaction);
            DbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a transaction with the provided id form the databse.
        /// </summary>
        /// <param name="id">The id of the transaction to delete.</param>
        /// <response code="200">The transaction was successfully deleted.</response>
        /// <response code="404">No transaction with the provided id could be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransactionAsync(int id)
        {
            // Get the transaction using the supplied id
            var transaction = await DbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
                return NotFound();

            // Remove the transaction from the database
            DbContext.Transactions.Remove(transaction);
            await DbContext.SaveChangesAsync();

            return Ok();
        }
    }
}