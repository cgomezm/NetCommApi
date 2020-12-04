using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiquorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You can view all alcoholic beverages");
        }

        [HttpPut]
        [Authorize(Policy = "AdminRoleRequired")]
        //[Authorize(Roles = "admin")]
        public IActionResult Update()
        {
            return Ok("You can update the liquor inv.");
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "OnlyColombian")]
        public IActionResult OnlyColombianLiquor()
        {
            return Ok("You can view Colombian liquor");
        }

        [HttpPost]
        [Authorize(Policy = "MayorDeEdad")] 
        public IActionResult Buy()
        {
            return Ok("You can buy alcoholic beverages");
        }


    }
}
