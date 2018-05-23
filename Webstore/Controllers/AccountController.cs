using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webstore.Data;
using Webstore.Data.Models;
using Webstore.ViewModels;

namespace Webstore.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly R0ga3cContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper,  R0ga3cContext dbcontext, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _appDbContext = dbcontext;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult("Hiba a létrehozáskor");

            await _appDbContext.Vevo.AddAsync(new Vevo {
                IdentityId = userIdentity.Id,
                Jelszo = userIdentity.Password,
                Email = userIdentity.Email,
                Nev=userIdentity.FirstName,
                Szamlaszam="111-1110011",
                Login = userIdentity.Email
            });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }*/
    }
}
