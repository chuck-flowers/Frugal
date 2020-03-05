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
    /// The controller responsible for managing the category entities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        /// <summary>
        /// The object used to interact with the database
        /// </summary>
        private FrugalContext DbContext { get; }

        /// <summary>
        /// The object used to map DTOs to database entities.
        /// </summary>
        private IMapper Mapper { get; }

        /// <summary>
        /// Constructs the controller with the required dependencies.
        /// </summary>
        /// <param name="dbContext">The object used to interact with the database.</param>
        /// <param name="mapper">The object used to map DTOs to database entities.</param>
        public CategoriesController(FrugalContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        /// <summary>
        /// Retrieves all categories in the database.
        /// </summary>
        /// <response code="200">Returns the list of categories</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<CategoryDto> GetAllCategoriesAsync() =>
            DbContext.Categories.Select(Mapper.Map<CategoryDto>);

        /// <summary>
        /// Retrieves the category matching an id.
        /// </summary>
        /// <param name="id">The id of the category to retrieve.</param>
        /// <response code="200">Returns the matching category.</response>
        /// <response code="404">No category with the id was found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Inserts a provided category into the database.
        /// </summary>
        /// <param name="category">The category to insert into the database.</param>
        /// <response code="201">The category was inserted into the database.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync(CategoryDto category)
        {
            var dbCategory = Mapper.Map<Category>(category);
            await DbContext.Categories.AddAsync(dbCategory);
            await DbContext.SaveChangesAsync();

            category.Id = dbCategory.Id;
            return CreatedAtAction(nameof(GetCategoryById), new { id = dbCategory.Id }, category);
        }

        /// <summary>
        /// Updates an category within the database.
        /// </summary>
        /// <param name="category">The category to update within the database.</param>
        /// <response code="200">The category was successfully updated.</response>
        /// <response code="404">No category with the id could be found.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCategoryAsync(CategoryDto category)
        {
            var existingCategory = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (existingCategory == null)
                return NotFound();

            Mapper.Map(category, existingCategory);
            await DbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Deletes a category with the provided id from the databse.
        /// </summary>
        /// <param name="id">The id of the category to delete.</param>
        /// <response code="200">The category was successfully deleted.</response>
        /// <response code="404">No category with the provided id could be found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            // Get the category using the supplied id
            var category = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return NotFound();

            // Remove the category from the database
            DbContext.Categories.Remove(category);
            await DbContext.SaveChangesAsync();

            return Ok();
        }
    }
}