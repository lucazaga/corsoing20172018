using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI
{
    [Route("api")]
    public class ServiceApiController : Controller
    {
        [HttpGet("getStudents")]
        public async Task<IActionResult> GetStudents()
        {
            await Task.Delay(100); //Simulate work

            return Ok(new dynamic[]
            {
                new { Name = "Alberto", Age=28 }
            });
        }
    }
}
