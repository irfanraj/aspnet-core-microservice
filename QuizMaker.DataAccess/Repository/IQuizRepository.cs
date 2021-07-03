using System;
using System.Collections.Generic;
using System.Text;
using QuizMaker.Abstractions.Models;

namespace QuizMaker.DataAccess.Repository
{
    public interface IQuizRepository
    {
        int AddQuiz(Quiz model);

        Quiz GetQuiz(int quizId);
    }
}
