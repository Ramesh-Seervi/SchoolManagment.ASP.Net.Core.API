using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.API.Models;
using SchoolManagment.API.Models.StaffModel;
using SchoolManagment.API.Models.TeacherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Controllers.StaffController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        public StaffController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new StaffModelQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("{StaffID}")]
        public async Task<IActionResult> GetOne(int StaffID)
        {
            await Db.Connection.OpenAsync();
            var query = new StaffModelQuery(Db);
            var result = await query.FindOneAsync(StaffID);
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
        public async Task<IActionResult> Post([FromBody] StaffModel body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/blog/5
        [HttpPut("{StaffID}")]
        public async Task<IActionResult> PutOne(int StaffID, [FromBody] StaffModel body)
        {
            await Db.Connection.OpenAsync();
            var query = new StaffModelQuery(Db);
            var result = await query.FindOneAsync(StaffID);
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
            result.StaffName = body.StaffName;
            result.StaffDepartment = body.StaffDepartment;
            result.StaffWork = body.StaffWork;
            result.StaffExperience = body.StaffExperience;
            result.StaffShift = body.StaffShift;
            result.StaffDOB = body.StaffDOB;
            result.StaffContactNo = body.StaffContactNo;
            result.StaffJoiningDate = body.StaffJoiningDate;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/blog/5
        [HttpDelete("{StaffID}")]
        public async Task<IActionResult> DeleteOne(int StaffID)
        {
            await Db.Connection.OpenAsync();
            var query = new StaffModelQuery(Db);
            var result = await query.FindOneAsync(StaffID);
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

