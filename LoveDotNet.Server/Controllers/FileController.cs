using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoveDotNet.Models;
using System;

namespace LoveDotNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public static string WebRootPath = "/root/website/LoveDotNet/LoveDotNet.Client/dist/";

        [HttpPost("Upload/{filepath}/{filename}")]
        public async Task<ActionResult<bool>> Upload(string filepath, string filename)
        {
            try
            {
                string path = WebRootPath + filepath + "/" + filename;
                using (var fileStream = System.IO.File.Create(path))
                {
                    await Request.Body.CopyToAsync(fileStream);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
