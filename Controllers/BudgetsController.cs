using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Constructs a new BudgetsController
        /// </summary>
        /// <param name="dbContext">The FrugalContext with which to update the databse.</param>
        public BudgetsController(FrugalContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<BudgetDto>> GetBudgetsAsync()
        {
            var dtoBudgets = DbContext.Budgets.Select(b => new BudgetDto(b.Name) { Id = b.Id });
            return await dtoBudgets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetByIdAsync(int id)
        {
            // Get the budget using the provided id
            var budget = await DbContext.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (budget == null)
                return NotFound();

            // Return the DTO version of the budget
            return Ok(new BudgetDto(budget.Name) { Id = budget.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudgetAsync(BudgetDto budget)
        {
            // Create the DB Model form of the budget
            var newBudget = new Budget(budget.Name);

            // Add and save the changes
            await DbContext.Budgets.AddAsync(newBudget);
            await DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBudgetByIdAsync), new { id = newBudget.Id }, newBudget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudgetAsync(int id, BudgetDto budget)
        {
            // Get the budget using the supplied id
            var existingBudget = await DbContext.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (existingBudget == null)
                return NotFound();

            // Update the DB model
            existingBudget.Name = budget.Name;
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