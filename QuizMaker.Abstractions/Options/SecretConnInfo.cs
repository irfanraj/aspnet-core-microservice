using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Abstractions.Options
{
    public class SecretConnInfo
    {
        public string HostName { get; set; }

        public string DatabaseName { get; set; }

        public string WebUser { get; set; } = string.Empty;

        public string WebUserPassword { get; set; } = string.Empty;
    }
}
