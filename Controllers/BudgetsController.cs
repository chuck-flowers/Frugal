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
    /// The controller responsible for managing the budget entities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
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
        /// Constructs a new BudgetsController
        /// </summary>
        /// <param name="dbContext">The FrugalContext with which to update the databse.</param>
        /// <param name="mapper">The mapper used to convert between DTOs and database models.</param>
        public BudgetsController(FrugalContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        /// <summary>
        /// Retrieves all the budgets
        /// </summary>
        /// <response code="200">Returns the list of budgets</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<BudgetDto> GetBudgetsAsync() => DbContext.Budgets.Select(Mapper.Map<Budget, BudgetDto>);

        /// <summary>
        /// Retrieves the budget matching an id.
        /// </summary>
        /// <param name="id">The id of the budget to fetch.</param>
        /// <response code="200">Returns the matching budget.</response>
        /// <response code="404">No budget with the id was found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BudgetDto>> GetBudgetByIdAsync(int id)
        {
            // Get the budget using the provided id
            var budget = await DbContext.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (budget == null)
                return NotFound();

            // Return the DTO version of the budget
            return Ok(Mapper.Map<BudgetDto>(budget));
        }

        /// <summary>
        /// Inserts a provided budget into the databse.
        /// </summary>
        /// <param name="budget">The budget to insert.</param>
        /// <response code="201">The budget was successfully inserted</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBudgetAsync(BudgetDto budget)
        {
            // Create the DB Model form of the budget
            Budget newBudget = Mapper.Map<Budget>(budget);

            // Add and save the changes
            await DbContext.Budgets.AddAsync(newBudget);
            await DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBudgetByIdAsync), new { id = newBudget.Id }, newBudget);
        }

        /// <summary>
        /// Updates a budget within the database.
        /// </summary>
        /// <param name="budget">The budget to update within the database.</param>
        /// <response code="200">The budget was successfully updated</response>
        /// <response code="404">No budget with the provided ID could be found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBudgetAsync(BudgetDto budget)
        {
            // Get the budget using the supplied id
            var existingBudget = await DbContext.Budgets.FirstOrDefaultAsync(b => b.Id == budget.Id);
            if (existingBudget == null)
                return NotFound();

            // Update the DB model
            Mapper.Map(budget, existingBudget);
            DbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a budget with the provided id form the databse.
        /// </summary>
        /// <param name="id">The id of the budget to delete.</param>
        /// <response code="200">The budget was successfully deleted.</response>
        /// <response code="404">No budget with the provided id could be found.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBudgetAsync(int id)
        {
            // Get the budget using the supplied id
            var budget = await DbContext.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (budget == null)
                return NotFound();

            // Remove the budget from the database
            DbContext.Budgets.Remove(budget);
            await DbContext.SaveChangesAsync();

            return Ok();
        }
    }
}