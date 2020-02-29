using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Frugal.Data;
using Frugal.Dtos;
using Frugal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Frugal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private FrugalContext DbContext { get; }

        private IMapper Mapper { get; }

        /// <summary>
        /// Constructs a new BudgetsController
        /// </summary>
        /// <param name="dbContext">The FrugalContext with which to update the databse.</param>
        public BudgetsController(FrugalContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<BudgetDto> GetBudgetsAsync() => DbContext.Budgets.Select(Mapper.Map<Budget, BudgetDto>);

        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetDto>> GetBudgetByIdAsync(int id)
        {
            // Get the budget using the provided id
            var budget = await DbContext.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (budget == null)
                return NotFound();

            // Return the DTO version of the budget
            return Ok(Mapper.Map<BudgetDto>(budget));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudgetAsync(BudgetDto budget)
        {
            // Create the DB Model form of the budget
            Budget newBudget = Mapper.Map<Budget>(budget);

            // Add and save the changes
            await DbContext.Budgets.AddAsync(newBudget);
            await DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBudgetByIdAsync), new { id = newBudget.Id }, newBudget);
        }

        [HttpPut("{id}")]
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

        [HttpDelete]
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