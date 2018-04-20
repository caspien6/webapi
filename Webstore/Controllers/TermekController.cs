using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webstore.Services;

namespace Webstore.Controllers
{
    [Produces("application/json")]
    [Route("api/Termek")]
    public class TermekController : Controller
    {
        private ITermekService _db;

        public TermekController(ITermekService db)
        {
            _db = db;
        }

        // GET: api/Termek
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_db.GetAll());
        }

        // GET: api/Termek/1
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var termek = _db.GetById(id);
                _db.IncreaseViews(id);
                return Ok(termek);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("byName")]
        public IActionResult getTermekByName(string name)
        {
            try
            {
                var termek = _db.GetByName(name);
                return Ok(termek);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpGet("byKategory")]
        public IActionResult getTermekByKategory(int katid)
        {
            try
            {
                var termek = _db.GetByKategoryId(katid);
                return Ok(termek);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }


        [Route("/error")]
        public IActionResult Index2()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Hibaa");
        }
        
    }
}