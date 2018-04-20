using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webstore.Services;

namespace Webstore.Controllers
{
    
    [Route("api/Kategory")]
    public class KategoryController : Controller
    {
        private IKategoryService _db;

        public KategoryController(IKategoryService db)
        {
            _db = db;
        }

        // GET: api/Kategory
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var list = _db.GetAll().ToList();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        // GET: api/Kategory/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var kategoria = _db.GetById(id);
                return Ok(kategoria);
            }
            catch (Exception)
            {
                return NotFound(id);
            }
        }

        // GET: api/Kategory/byName?name=valami
        [HttpGet("byName")]
        public IActionResult Get(string name)
        {
            try
            {
                var kategoria = _db.GetByName(name);
                return Ok(kategoria);
            }
            catch (Exception)
            {
                return NotFound(name);
            }
        }

        // POST: api/Kategory
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Kategory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
