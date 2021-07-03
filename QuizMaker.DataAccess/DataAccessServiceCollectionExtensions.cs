using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QuizMaker.DataAccess.Repository;

namespace QuizMaker.DataAccess
{
    /// <summary>
    /// Registers Dependency injections for DataAcess classes those will be constructor injected in QuizMaker.Service proj.
    /// </summary>
    public static class DataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IQuizRepository), typeof(QuizRepository));
            
            return services;
        }
    }
}
