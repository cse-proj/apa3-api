using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APA3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult SignIn()
        {
            return Ok();
        }

        [Route("signup")]
        [HttpPost]
        public IActionResult SignUp()
        {
            return Ok();
        }

        [Route("resetPassword")]
        [HttpPost]
        public IActionResult ResetPassword()
        {
            return Ok();
        }

        [Route("updatePassword")]
        [HttpPost]
        public IActionResult UpdatePassword()
        {
            return Ok();
        }
    }
}
