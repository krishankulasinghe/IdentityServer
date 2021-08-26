using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
       private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        public IActionResult Create()
        {
            return Ok(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            var role = new IdentityRole { Name = model.Name };

           var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add role");
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
