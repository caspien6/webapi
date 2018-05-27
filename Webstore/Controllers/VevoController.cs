using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class VevoController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private R0ga3cContext _dbcontext;
        private readonly ILogger<VevoController> _logger;

        public VevoController(R0ga3cContext dbcontext, ILogger<VevoController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _dbcontext = dbcontext;
            _logger = logger;
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Vevo), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVevo(string name)
        {
            try
            {
                // retrieve the user info
                //HttpContext.User
                var userId = _caller.Claims.Single(c => c.Type == "id");
                var vevo = await _dbcontext.Vevo.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);               
                return Ok(vevo);
            }
            catch (EntityNotFoundException e)
            {
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