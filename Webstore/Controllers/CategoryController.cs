using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webstore.Data.Models;
using Webstore.OwnExceptions;
using Webstore.Services;

namespace Webstore.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : Controller
    {
        private IKategoryService _db;
        private readonly ILogger _logger;

        public CategoryController(IKategoryService db, ILogger<CategoryController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Az összes kategória lekérése.
        /// </summary>
        /// <returns></returns>
        // GET: api/Kategory
        [HttpGet]
        [ProducesResponseType(typeof(Kategoria[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var list = _db.GetAll().ToList();
                _logger.LogInformation("Finding all category");
                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        // GET: api/Kategory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Kategoria), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Finding category by id: {id}");
            try
            {
                var kategoria = _db.GetById(id);
                _logger.LogInformation($"Category founded.");
                return Ok(kategoria);
            }
            catch(EntityNotFoundException e)
            {
                _logger.LogInformation($"Category not founded.");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Kategory/byName?name=valami
        [HttpGet("byName")]
        [ProducesResponseType(typeof(Kategoria[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(string name)
        {
            _logger.LogInformation($"Finding category by name: {name}");
            try
            {
                var kategoria = _db.GetByName(name);
                _logger.LogInformation($"Category founded.");
                return Ok(kategoria);
            }
            catch (EntityNotFoundException e)
            {
                _logger.LogInformation($"Category not founded.");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
    }
}
