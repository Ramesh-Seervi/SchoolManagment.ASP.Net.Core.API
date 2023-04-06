using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.API.Models;
using SchoolManagment.API.Models.TeacherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Controllers.TeacherController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        public TeacherController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new TeacherModelQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("{TeacherID}")]
        public async Task<IActionResult> GetOne(int TeacherID)
        {
            await Db.Connection.OpenAsync();
            var query = new TeacherModelQuery(Db);
            var result = await query.FindOneAsync(TeacherID);
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
        public async Task<IActionResult> Post([FromBody] TeacherModel body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/blog/5
        [HttpPut("{TeacherID}")]
        public async Task<IActionResult> PutOne(int TeacherID, [FromBody] TeacherModel body)
        {
            await Db.Connection.OpenAsync();
            var query = new TeacherModelQuery(Db);
            var result = await query.FindOneAsync(TeacherID);
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
            result.TeacherName = body.TeacherName;
            result.TeacherDOB = body.TeacherDOB;
            result.TeacherGender = body.TeacherGender;
            result.TeacherSubject = body.TeacherSubject;
            result.TeacherContactNo = body.TeacherContactNo;
            result.TeacherJoiningDate = body.TeacherJoiningDate;
            result.Addresss = body.Addresss;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/blog/5
        [HttpDelete("{TeacherID}")]
        public async Task<IActionResult> DeleteOne(int TeacherID)
        {
            await Db.Connection.OpenAsync();
            var query = new TeacherModelQuery(Db);
            var result = await query.FindOneAsync(TeacherID);
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
