using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Abstractions.Options
{
    public class AwsSettingOptions
    {
        /// <summary>
        /// Gets Secret key from Settings file.
        /// </summary>
        public string SecretKey { get; set; }
    }
}
