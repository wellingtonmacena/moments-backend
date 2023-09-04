using Microsoft.AspNetCore.Mvc;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Controllers
{
    [ApiController]
    [Route("api/v1/comments")]
    public class CommentsController : Controller
    {
        private ICommentRepository _postgresMomentRepository { get; set; }
        public CommentsController(ICommentRepository postgresCommentRepository)
        {
            _postgresMomentRepository = postgresCommentRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOne([FromForm] Comment comment)
        {
            if(comment.MomentId == null)
                StatusCode(406, "MomentId is null or empty");
            _postgresMomentRepository.CreateOne(comment);

            return Created("", comment);
        }
    }
}
