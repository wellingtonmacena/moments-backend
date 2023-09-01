﻿using Microsoft.AspNetCore.Mvc;
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
        private IConfiguration _configuration { get; set; }
        private IHandleFile _iHandleFileService { get; set; }
        public MomentsController(IMomentRepository postgresMomentRepository, IConfiguration configuration, IHandleFile iHandleFileService)
        {
            _postgresMomentRepository = postgresMomentRepository;
            _configuration = configuration;
            _iHandleFileService = iHandleFileService;
        }

        [HttpGet]
        [Route("/{id}")]
        public ActionResult GetOne([FromRoute] int id)
        {
            Moment moment = _postgresMomentRepository.GetOne(id);

            if (moment == null)
                return NotFound();
            else
                return Ok(moment);
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
        public async Task<ActionResult> CreateOne(IFormFile imageFile, [FromForm] Moment moment)
        {
            if (!imageFile.ContentType.Contains("image"))
                return StatusCode(415, new { Message = "Only accepts image type file" });

            HandleFileDTO handleFileDTO = await _iHandleFileService.Save(imageFile);
            moment.ImageURL = handleFileDTO.ImageURL;
            moment.ImagePath = handleFileDTO.ImagePath;

            _postgresMomentRepository.CreateOne(moment);

            return Created("", moment);
        }

        [HttpPut]
        public ActionResult UpdateOne([FromForm] Moment moment)
        {
            bool wasUpdated = _postgresMomentRepository.UpdateOne(moment);

            if (!wasUpdated)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete]
        [Route("/{id}")]
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
        //    // return Ok(_postgresCommentRepository.DeleteAll());
        //}
    }
}