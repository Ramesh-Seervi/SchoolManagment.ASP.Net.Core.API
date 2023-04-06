using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Controllers
{
    [Route("api/[controller]")]
    
    public class StudentController : ControllerBase
    {
        public StudentController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new StudentModelQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("{StudentRollno}")]
        public async Task<IActionResult> GetOne(int StudentRollno)
        {
            await Db.Connection.OpenAsync();
            var query = new StudentModelQuery(Db);
            var result = await query.FindOneAsync(StudentRollno);
            if (result is null)
            {
                return Ok(new StatusReply
                {
                    Status = false,
                    Message = "Student Not Found",
                    Title = "Roll Number - Error",
                    StatusCode = StatusCodes.Status200OK
                });
            }
            return new OkObjectResult(result);
        }

        // POST api/blog
        [HttpPost]
        public async Task<IActionResult> PostStd([FromBody] StudentModel body)
        {
            try
            {
                await Db.Connection.OpenAsync();
                body.Db = Db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
            
        }

        // PUT api/blog/5
        [HttpPut("{StudentRollno}")]
        public async Task<IActionResult> PutOne(int StudentRollno, [FromBody] StudentModel body)
        {
            await Db.Connection.OpenAsync();
            var query = new StudentModelQuery(Db);
            var result = await query.FindOneAsync(StudentRollno);
            if (result is null)
            {
                return Ok(new StatusReply
                {
                    Status = false,
                    Message = "Roll Number is in Correct",
                    Title = "Roll Number is Not Unavailable - Error",
                    StatusCode = StatusCodes.Status200OK
                });
            }
            result.StudentName = body.StudentName;
            result.StudentDOB = body.StudentDOB;
            result.StudentClass = body.StudentClass;
            result.StudentContactNo = body.StudentContactNo;
            result.StudentGender = body.StudentGender;
            result.StudentJoiningDate = body.StudentJoiningDate;
            result.StudentParentName = body.StudentParentName;
            result.StudentParentNumber = body.StudentJoiningDate;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/blog/5
        [HttpDelete("{StudentRollno}")]
        public async Task<IActionResult> DeleteOne(int StudentRollno)
        {
            await Db.Connection.OpenAsync();
            var query = new StudentModelQuery(Db);
            var result = await query.FindOneAsync(StudentRollno);
            if (result is null)
            {
                return Ok(new StatusReply
                {
                    Status = false,
                    Message = "Student Not Available",
                    Title = "Roll Number is Not Unavailable - Error",
                    StatusCode = StatusCodes.Status200OK

                });
            }
            await result.DeleteAsync(); 
            return Ok(new StatusReply
            {
                Status = true,
                Message = "Data Deleted...",
                Title = "Deleted Successfully",
                StatusCode = StatusCodes.Status200OK

            });
        }


        public AppDb Db { get; }
    }
}
