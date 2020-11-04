using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APA3.Drive.Services;
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
        private readonly GoogleDriveService _drive;

        public DriveController(ILogger<DriveController> logger, GoogleDriveService drive)
        {
            _logger = logger;
            _drive = drive;
        }

        
        [Route("")]
        [Route("folders")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _drive.GetFolders());
        }

        [Route("folders/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetFolder(string id)
        {
            return Ok(await _drive.GetFolder(id));
        }

        [HttpGet("files/{id}")]
        public async Task<IActionResult> GetFiles(string id)
        {
            return Ok(await _drive.GetFile(id));
        }
    }
}
