using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webstore.Data.Models;
using Webstore.OwnExceptions;
using Webstore.Services;


namespace Webstore.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TermekController : Controller
    {
        private ITermekService _db;
        private readonly ILogger _logger;

        public TermekController(ITermekService db , ILogger<TermekController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Az összes megtalálható terméket visszaadja
        /// </summary>
        /// <returns></returns>
        // GET: api/Termek
        [HttpGet]
        [ProducesResponseType(typeof(Termek[]), 200)]
        public IActionResult Index()
        {
            return Ok(_db.GetAll());
        }

        // GET: api/Termek/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Termek), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetTermekById(int id)
        {
            _logger.LogInformation($"Getting termek details by id: {id}");
            try
            {
                var termek = _db.GetById(id);
                _db.IncreaseViews(id);
                _logger.LogInformation($"Entity found return ok", termek);
                return Ok(termek);
            }
            catch (EntityNotFoundException e)
            {
                _logger.LogInformation($"Entity not found!", e);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("byName")]
        [ProducesResponseType(typeof(Termek[]),200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetTermekByName(string name)
        {
            _logger.LogInformation($"Getting termek details by name: {name}");
            try
            {
                var termek = _db.GetByName(name);
                _logger.LogInformation($"Entity found return ok", termek);
                return Ok(termek);
            }
            catch (EntityNotFoundException e)
            {
                _logger.LogInformation($"Entity not found!", e);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(500);
            }

        }

        [HttpGet("byKategory")]
        [ProducesResponseType(typeof(Termek[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetTermekByKategory(int katid)
        {
            _logger.LogInformation($"Getting termek details by kategoria id: {katid}");
            try
            {
                var termek = _db.GetByKategoryId(katid);
                _logger.LogInformation($"Entity found return ok", termek);
                return Ok(termek);
            }
            catch (EntityNotFoundException e)
            {
                _logger.LogInformation($"Entity not found!", e);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(500);
            }
        }
        
    }
}