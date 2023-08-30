using Microsoft.AspNetCore.Mvc;
using Moments_Backend.Interfaces;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Controllers
{
    [ApiController]
    [Route("api/v1/moments")]
    public class MomentsController : Controller
    {
        private IMomentRepository _postgresMomentRepository { get; set; }
        private IConfiguration _configuration { get; set; }
        private ISaveFile _iSaveFileService { get; set; }
        public MomentsController(IMomentRepository postgresMomentRepository, IConfiguration configuration, ISaveFile iSaveFile)
        {
            _postgresMomentRepository = postgresMomentRepository;
            _configuration = configuration;
            _iSaveFileService = iSaveFile;
        }

        [HttpGet]
        [Route("/{id}")]
        public ActionResult GetOne([FromRoute] int id)
        {
            Moment moment = _postgresMomentRepository.GetOne(id);

            if (moment == null)
                return NotFound();
            else
                return Ok( moment);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Moment> moments = _postgresMomentRepository.GetAll();

            if (moments.Any())
                return Ok(moments);
            else
                return NoContent();
        }

        [HttpPost]
        public ActionResult CreateOne(IFormFile imageFile, [FromForm] Moment moment)
        {
            if (!imageFile.ContentType.Contains("image"))
                return StatusCode(415, new { Message = "Only accepts image type file" });

            moment.ImageURL = _iSaveFileService.Execute(imageFile).Result;
            _postgresMomentRepository.CreateOne(moment);

            return Created("", moment);
        }

        [HttpPut]
        public ActionResult UpdateOne([FromForm] Moment moment)
        {
            bool wasUpdated = _postgresMomentRepository.UpdateOne(moment);

            if (wasUpdated)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete]
        [Route("/{id}")]
        public ActionResult DeleteOne([FromRoute] int id)
        {
            bool moment = _postgresMomentRepository.DeleteOne(id);

            if (!moment)
                return NotFound();
            else
                return Ok();
        }

        //[HttpDelete]
        //public ActionResult DeleteAll()
        //{
        //    // return Ok(_postgresCommentRepository.DeleteAll());
        //}
    }
}
