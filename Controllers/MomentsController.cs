using Microsoft.AspNetCore.Mvc;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;
using Moments_Backend.Services;

namespace Moments_Backend.Controllers
{
    [ApiController]
    [Route("api/v1/moments")]
    public class MomentsController : Controller
    {
        private IMomentRepository _postgresCommentRepository { get; set; }
        private IConfiguration _configuration { get; set; }
        public MomentsController(IMomentRepository postgresCommentRepository, IConfiguration configuration)
        {
            _postgresCommentRepository = postgresCommentRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_postgresCommentRepository.GetAll());
        }

        [HttpPost]

        public ActionResult CreateOne(IFormFile imageFile, [FromForm] Moment moment)
        {
            SaveFileService saveFileService = new SaveFileService(_configuration);

            string imageUrl = saveFileService.Execute(imageFile).Result ;
            moment.ImageURL = imageUrl;

            return null;
            //return Ok(_postgresCommentRepository.CreateOne((Moment)moment));
        }

        [HttpDelete]
        public ActionResult DeleteAll()
        {
            return Ok(_postgresCommentRepository.DeleteAll());
        }
    }
}
