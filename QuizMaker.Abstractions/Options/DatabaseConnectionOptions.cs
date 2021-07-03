using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Abstractions.Options
{
    public class DatabaseConnectionOptions
    {
        /// <summary>
        /// connection string built from reading information from Secret Key.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
