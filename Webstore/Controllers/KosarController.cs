using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class KosarController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private IKosarService _kosarService;
        private R0ga3cContext _dbcontext;
        private readonly ILogger<KosarController> _logger;

        public KosarController(R0ga3cContext dbcontext, IKosarService kosarService, IHttpContextAccessor httpContextAccessor, ILogger<KosarController> logger)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _dbcontext = dbcontext;
            _logger = logger;
            _kosarService = kosarService;
        }

        [HttpGet("byVevoId={vevoId}")]
        [ProducesResponseType(typeof(Kosar[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetKosars(int vevoId)
        {
            try
            {
                // retrieve the user info
                //HttpContext.User
                var userId = _caller.Claims.Single(c => c.Type == "id");
                var vevo = _dbcontext.Vevo.Include(c => c.Identity).Include(c => c.Kosar).Single(c => c.Identity.Id == userId.Value);
                var kosar = _dbcontext.Kosar.Include(k => k.KosarTetel).Where(k => k.VevoId == vevo.Id).ToArray();
                return Ok(kosar);
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
                // retrieve the user info
                //HttpContext.User
                var userId = _caller.Claims.Single(c => c.Type == "id");
                var vevo = _dbcontext.Vevo.Include(c => c.Identity).Include(c => c.Kosar).Single(c => c.Identity.Id == userId.Value);
                var kosar = _dbcontext.Kosar.Include(k => k.KosarTetel).Where(k => k.VevoId == vevo.Id).ToArray();
                _kosarService.AddKosarTetel(kosar.ElementAt(0).Id, termek, mennyiseg);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ProductQuantityException e)
            {
                _logger.LogError(e.StackTrace);
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