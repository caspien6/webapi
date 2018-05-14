﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webstore.Data.Models;
using Webstore.OwnExceptions;
using Webstore.Services;

namespace Webstore.Controllers
{
    [Produces("application/json")]
    [Route("api/Vevo")]
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

        [HttpPost("name={name}&email={email}&szamlasz={szamla}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult PostVevo(string name, string email, string szamla)
        {
            try
            {
                _vevoService.CreateVevo(name, email, szamla);
                return Ok();
            }
            catch (EntityAlreadyExistsException e)
            {
                return BadRequest(e.Message); 
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return StatusCode(500);
            }
        }

    }
}