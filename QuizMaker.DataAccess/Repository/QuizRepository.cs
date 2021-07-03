using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizMaker.Abstractions.Models;
using QuizMaker.Abstractions.Options;
using Dapper;
using Npgsql;

namespace QuizMaker.DataAccess.Repository
{
    public class QuizRepository : IQuizRepository
    {
        IList<Quiz> quizList = new List<Quiz>();
        private string connectionString = string.Empty;
        public QuizRepository(IOptions<DatabaseConnectionOptions> optionsAccessor)
            
        {
            if (optionsAccessor.Value != null)
            {
                connectionString = optionsAccessor.Value.ConnectionString;
            }
        }

        public NpgsqlConnection GetDatabaseConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// save quiz into database by executing insert query.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddQuiz(Quiz model)
        {
            //using statement will automatically dispose connection object once it goes out of scope.
            using var con = GetDatabaseConnection();
            con.Open();

            var sql = @"Insert into public.quiz(question, reward_point, expire_date) 
                        values(@question, @rewardPoint, @expireDate)";

            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("question", model.Question);
            cmd.Parameters.AddWithValue("rewardPoint", model.RewardPoint);
            cmd.Parameters.AddWithValue("expireDate", model.ExpireDate);
            cmd.Prepare();

            int affectedRows = cmd.ExecuteNonQuery();
            
            return affectedRows;
        }

        /// <summary>
        /// Retrieving quiz info from database by executing sql query.
        /// Returned resultset will be mapped to entity class by using Dapper
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public Quiz GetQuiz(int quizId)
        {
            var sql = @"select * from public.quiz where id = @quiz_id";
            using var con = GetDatabaseConnection();
           
            var retQuizList = con.Query<Quiz>(sql, new { quiz_id = quizId }).ToList();

            Quiz quiz = retQuizList.FirstOrDefault(x => x.Id == quizId);

            return quiz;
        }
    }
}
