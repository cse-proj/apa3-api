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
    public class DriveController : ControllerBase
    {
        private readonly ILogger<DriveController> _logger;

        public DriveController(ILogger<DriveController> logger)
        {
            _logger = logger;
        }

        [Route("folders")]
        [Route("")]
        [HttpGet]
        public IActionResult GetFolders()
        {
            return Ok();
        }

        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            return Ok();
        }

        [HttpGet("files/{id}/view")]
        public IActionResult GetFileViewLink(string id)
        {
            return Ok(id);
        }
    }
}
