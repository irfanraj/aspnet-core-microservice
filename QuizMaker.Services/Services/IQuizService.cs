using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizMaker.Abstractions.Models;

namespace QuizMaker.Services.Services
{
    public interface IQuizService
    {
       int CreateQuiz(Quiz quizModel);

       Quiz GetQuiz(int quizId);
    }
}
