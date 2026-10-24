using Microsoft.AspNetCore.Mvc;

namespace Atlas.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetStatus()
        {
            
            return Ok(new { message = "Бэкенд Atlas успешно работает на .NET 10!", timestamp = DateTime.Now });
        }
    }
}