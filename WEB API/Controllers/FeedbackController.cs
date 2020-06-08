using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMapper mapper;
        private IFeedback feedback { get; set; }
        public FeedbackController(IFeedback feedback, IMapper mapper)
        {
            this.feedback = feedback;
            this.mapper = mapper;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<long>  PostFeedback(APIFeedbackModel feed)
        {
            Feedback feedback = new Feedback();
            long newFeedbackId;
            try
            {
                newFeedbackId = feedback.InsertFeedback(mapper.Map<FeedbackModel>(feed));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (newFeedbackId == 0) return BadRequest("An error occured");
            return Created("", "The feedback is successfully added");
        }
    }
}