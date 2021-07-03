using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizMaker.Abstractions.Models;
using QuizMaker.Common;
using QuizMaker.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using QuizMaker.DataAccess.Repository;
using Microsoft.Extensions.Options;
using QuizMaker.Abstractions.Options;
using Microsoft.Extensions.Configuration;

namespace QuizMaker.Controllers
{
    [Route(QuizMakerConstants.DefaultRoutePath)]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;
        private readonly IQuizService _quizService;

        public  QuizController(IQuizService quizService, ILogger<QuizController> logger)
        {
            _quizService = quizService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateQiz([FromBody] Quiz quizModel)
        {
                        
            _quizService.CreateQuiz(quizModel);

            return Ok();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Quiz> GetQuiz(int id)
        {
            Quiz quiz = _quizService.GetQuiz(id);

            if(quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }


    }
}
