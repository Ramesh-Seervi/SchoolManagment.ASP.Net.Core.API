using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SchoolManagment.API.Controllers
{
    public class StatusReply 
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }

        
    }
}