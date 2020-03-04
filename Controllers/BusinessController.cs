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
    /// The controller responsible for managing the business entities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
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
        /// Constructs a new BusinessController
        /// </summary>
        /// <param name="dbContext">The FrugalContext with which to update the databse.</param>
        /// <param name="mapper">The mapper used to convert between DTOs and database models.</param>
        public BusinessController(FrugalContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        /// <summary>
        /// Retrieves all the businesses
        /// </summary>
        /// <response code="200">Returns the list of businesses</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<BusinessDto> GetBusinessesAsync() => DbContext.Businesses.Select(Mapper.Map<Business, BusinessDto>);

        /// <summary>
        /// Retrieves the business matching an id.
        /// </summary>
        /// <param name="id">The id of the business to fetch.</param>
        /// <response code="200">Returns the matching business.</response>
        /// <response code="404">No business with the id was found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BusinessDto>> GetBusinessByIdAsync(int id)
        {
            // Get the business using the provided id
            var business = await DbContext.Businesses.FirstOrDefaultAsync(b => b.Id == id);
            if (business == null)
                return NotFound();

            // Return the DTO version of the business
            return Ok(Mapper.Map<BusinessDto>(business));
        }

        /// <summary>
        /// Inserts a provided business into the databse.
        /// </summary>
        /// <param name="business">The business to insert.</param>
        /// <response code="201">The business was successfully inserted</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBusinessAsync(BusinessDto business)
        {
            // Create the DB Model form of the business
            Business newBusiness = Mapper.Map<Business>(business);

            // Add and save the changes
            await DbContext.Businesses.AddAsync(newBusiness);
            await DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBusinessByIdAsync), new { id = newBusiness.Id }, newBusiness);
        }

        /// <summary>
        /// Updates a business within the database.
        /// </summary>
        /// <param name="business">The business to update within the database.</param>
        /// <response code="200">The business was successfully updated</response>
        /// <response code="404">No business with the provided ID could be found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessAsync(BusinessDto business)
        {
            // Get the business using the supplied id
            var existingBusiness = await DbContext.Businesses.FirstOrDefaultAsync(b => b.Id == business.Id);
            if (existingBusiness == null)
                return NotFound();

            // Update the DB model
            Mapper.Map(business, existingBusiness);
            DbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a business with the provided id form the databse.
        /// </summary>
        /// <param name="id">The id of the business to delete.</param>
        /// <response code="200">The business was successfully deleted.</response>
        /// <response code="404">No business with the provided id could be found.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBusinessAsync(int id)
        {
            // Get the business using the supplied id
            var business = await DbContext.Businesses.FirstOrDefaultAsync(b => b.Id == id);
            if (business == null)
                return NotFound();

            // Remove the business from the database
            DbContext.Businesses.Remove(business);
            await DbContext.SaveChangesAsync();

            return Ok();
        }
    }
}