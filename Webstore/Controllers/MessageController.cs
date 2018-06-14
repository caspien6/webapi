using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Webstore.Data.Models;
using Webstore.SignalHubs;
using Webstore.SignalHubs.MessageModel;

namespace Webstore.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        private R0ga3cContext _dbcontext;
        private readonly ClaimsPrincipal _caller;

        public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext, IHttpContextAccessor httpContextAccessor, R0ga3cContext dbcontext)
        {
            _dbcontext = dbcontext;
            _hubContext = hubContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post([FromBody]Message msg)
        {
            string retMessage = string.Empty;
            try
            {
                var userId = _caller.Claims.Single(c => c.Type == "id");
                var vevo = _dbcontext.Vevo.Include(c => c.Identity).Single(c => c.Identity.Id == userId.Value);
                _hubContext.Clients.All.BroadcastMessage(msg.Type, $"{vevo.Nev}: {msg.Payload}");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                return Content(e.StackTrace);
            }
            return Content(retMessage);
        }
    }
}