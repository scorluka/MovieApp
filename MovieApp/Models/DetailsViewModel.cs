using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class DetailsViewModel
    {
        public int Movie_ID { get; set; }
        public string Title { get; set; }
        public string Cover_img { get; set; }
        public string Description { get; set; }
        public DateTime Release_date { get; set; }

        public string Release_dateFormatted { get { return this.Release_date.ToString("dd/MM/yyyy"); } }

        public string Cast { get; set; }

        public List<Movie> Movies { get; set; }
        public List<Movie_cast> Movie_casts { get; set; }
    }
}