using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.API.Models;
using SchoolManagment.API.Models.RegistersigninModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Controllers.RegistersigninController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public RegisterController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new RegisterQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("SignIn")]
        public async Task<IActionResult> GetOne(string Email, string Password)
        {
            await Db.Connection.OpenAsync();
            var query = new RegisterQuery(Db);
            var result = await query.FindOneAsync(Email, Password);
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
           else
            {
                result.UserActive = true;
            }
            return new OkObjectResult(result);
        }

        // POST api/blog
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.Registration();
            return new OkObjectResult(body);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] Register register)
        {
            await Db.Connection.OpenAsync();
            register.Db = Db;
            await register.Login();
            return new OkObjectResult(register);
        }
        // PUT api/blog/5
        [HttpPut("{StaffID}")]
        public async Task<IActionResult> PutOne(string Email, string Password, [FromBody] Register body)
        {
            await Db.Connection.OpenAsync();
            var query = new RegisterQuery(Db);
            var result = await query.FindOneAsync(Email, Password);
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
            result.UserName = body.UserName;
            result.Email = body.Email;
            result.Mobile = body.Mobile;
            result.Password = body.Password;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/blog/5
        [HttpDelete("{StaffID}")]
        public async Task<IActionResult> DeleteOne(string Email, string Password)
        {
            await Db.Connection.OpenAsync();
            var query = new RegisterQuery(Db);
            var result = await query.FindOneAsync(Email, Password);
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
