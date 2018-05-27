using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class VevoController : Controller
    {
        private IVevoService _vevoService;
        private readonly ILogger<VevoController> _logger;

        public VevoController(IVevoService vevoService, ILogger<VevoController> logger)
        {
            _vevoService = vevoService;
            _logger = logger;
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Vevo), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetVevo(string name)
        {
            try
            {
                var vevo = _vevoService.FindVevo(name);
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