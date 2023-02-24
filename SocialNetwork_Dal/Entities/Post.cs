using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialNetwork_Dal.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Provide Some Text")]
    
        public string PostText { get; set; }
        public HttpPostedFileBase PostImage { get; set; }
        public string PostImagePath { get; set; }

        public int PosterId { get; set; }

        public DateTime PostTime { get; set; }
        public string PosterName { get; set; }
        public string PosterImagePath { get; set; }
        public int Likes { get; set; }
    }
}
