using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Dal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ServiceAPI
{
    [Route("api")]
    public class ServiceApiController : Controller
    {
        static List<Student> students = new List<Student>(new[] { new Student { Id = 1, Name = "Alberto", DateOfBirth = new DateTime(1989, 5, 4) } });

        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            await Task.Delay(100); //Simulate work

            return Ok(students);
        }

        [HttpGet("student")]
        public async Task<IActionResult> GetStudent([FromQuery]int id)
        {
            return Ok(students.FirstOrDefault(s => s.Id == id));
        }

        [HttpPut("students")]
        public async Task<IActionResult> CreateStudent([FromBody]Student student)
        {
            students.Add(student);

            return Ok();
        }

        [HttpPost("students")]
        public async Task<IActionResult> UpdateStudent([FromBody]Student student)
        {
            return Ok();
        }

        [HttpDelete("students")]
        public async Task<IActionResult> DeleteStudent([FromQuery]int id)
        {
            students.RemoveAll(s => s.Id == id);
            return Ok();
        }
    }
}
