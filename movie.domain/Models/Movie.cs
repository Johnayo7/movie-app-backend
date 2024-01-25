using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie.domain.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public double ImdbScore { get; set; }


    }
}
