using Microsoft.AspNetCore.Mvc;

namespace Moments_Backend.Controllers
{
    [ApiController]
    [Route("api/v1/comments")]
    public class CommentsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
