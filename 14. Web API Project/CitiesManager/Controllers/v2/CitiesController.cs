using Asp.Versioning;
using CitiesManager.DatabaseContext;
using CitiesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Controllers.v2
{
    [ApiVersion("2.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CitiesComponent
        /// <summary>
        /// To get list of cities (only city name) from "cities" table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            var cities = await _context.Cities
                .OrderBy(temp => temp.CityName)
                .Select(temp => temp.CityName)
                .ToListAsync();
            return cities;
        }
    }
}