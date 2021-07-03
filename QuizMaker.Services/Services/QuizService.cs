using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizMaker.Abstractions.Models;
using QuizMaker.DataAccess.Repository;

namespace QuizMaker.Services.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository,
            ILogger<QuizService> logger)
        {
            _quizRepository = quizRepository;
            logger.LogInformation("Quiz-Service instantiated.");
        }

        /// <summary>
        /// Create Quiz.
        /// </summary>
        /// <param name="quizModel"></param>
        /// <returns>Newly created quiz.</returns>
        public int CreateQuiz(Quiz quizModel)
        {
            int rows = _quizRepository.AddQuiz(quizModel);

            return rows;
        }


        public Quiz GetQuiz(int quizId)
        {
            Quiz quiz = _quizRepository.GetQuiz(quizId);

            return quiz;
        }
    }
}
