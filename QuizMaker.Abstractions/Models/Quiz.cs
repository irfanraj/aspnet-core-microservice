using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Abstractions.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:2000, MinimumLength = 10)]
        public string Question { get; set; }

        [Required]
        [Range(1,1000)]
        [Display(Name = "Rewards Points")]
        public int RewardPoint { get; set; }

        public DateTime ExpireDate { get; set; }

        public IList<Answer> Answers { get; set; } = new List<Answer>();
    }
}
