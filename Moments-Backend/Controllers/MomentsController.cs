using Microsoft.AspNetCore.Mvc;
using Moments_Backend.Interfaces;
using Moments_Backend.Models;
using Moments_Backend.Models.DTOs;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Controllers
{
    [ApiController]
    [Route("api/v1/moments")]
    public class MomentsController : Controller
    {
        private IMomentRepository _postgresMomentRepository { get; set; }
        private IHandleFile _iHandleFileService { get; set; }
        public MomentsController(IMomentRepository postgresMomentRepository, IHandleFile iHandleFileService)
        {
            _postgresMomentRepository = postgresMomentRepository;
            _iHandleFileService = iHandleFileService;
        }

        [HttpGet]
        [Route("/test")]
        public ActionResult GetTest()
        {
            return Ok(new { data = "teste" });
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOne([FromRoute] int id)
        {
            Moment moment = _postgresMomentRepository.GetOne(id);

            if (moment == null)
                return NotFound();
            else
                return Ok(new { data = moment });
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Moment> moments = _postgresMomentRepository.GetAll();

            if (moments.Any())
                return Ok(new { data = moments });
            else
                return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOne([FromForm] IFormFile image, [FromForm] Moment moment)
        {
            if (!image.ContentType.Contains("image"))
                return StatusCode(415, new { Message = "Only accepts image type file" });

            HandleFileDTO handleFileDTO = await _iHandleFileService.Save(image);
            moment.SetImageInfo(handleFileDTO);
            moment.SetCreationInfo();

            await _postgresMomentRepository.CreateOne(moment);

            return Created("", moment);
        }

        [HttpPost]
        [Route("{id}/comments")]
        public async Task<ActionResult> CreateOneComment([FromBody] Comment comment)
        {
            if (comment.MomentId == null)
                StatusCode(406, "MomentId is null or empty");

            comment.SetCreationInfo();
            await _postgresMomentRepository.CreateOneComment(comment);

            return Created("", new { data = comment });
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateOne([FromForm] Moment moment)
        {
            bool wasUpdated = _postgresMomentRepository.UpdateOne(moment);

            if (!wasUpdated)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteOne([FromRoute] int id)
        {
            Moment moment = _postgresMomentRepository.DeleteOne(id);

            if (moment == null)
            {
                return NotFound();
            }
            else
            {
                _iHandleFileService.Delete(moment.ImagePath);
                return Ok();
            }
        }

        //[HttpDelete]
        //public ActionResult DeleteAll()
        //{
        //    try
        //    {
        //        _postgresMomentRepository.DeleteAll();
        //        _iHandleFileService.DeleteAll();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex);
        //    }
        //}
    }
}
