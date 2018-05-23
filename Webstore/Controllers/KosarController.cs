using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class KosarController : Controller
    {
        private IKosarService _kosarService;
        private readonly ILogger<KosarController> _logger;

        public KosarController(IKosarService kosarService, ILogger<KosarController> logger)
        {
            _kosarService = kosarService;
            _logger = logger;
        }

        [HttpGet("byVevoId={vevoId}")]
        [ProducesResponseType(typeof(Kosar[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetKosars(int vevoId)
        {
            try
            {
                var kosars = _kosarService.FindKosars(vevoId);
                return Ok(kosars);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost("kosarid={kosarId}&mennyiseg={mennyiseg}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(406)]
        [ProducesResponseType(500)]
        [ActionName("Add new termek")]
        public IActionResult PostTermek(int kosarId, [FromBody]Termek termek, int mennyiseg)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _kosarService.AddKosarTetel(kosarId, termek, mennyiseg);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ProductQuantityException e)
            {
                return StatusCode(406);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(500);
            }
        }
    }
}