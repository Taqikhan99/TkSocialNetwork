
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialNetwork_Dal.Entities
{
    public class User
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="First name is required!"), MaxLength(30)]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Last name is required!"), MaxLength(30)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User name is required!"), MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required!")]
        public string Phone  { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Dob { get; set; }
        public HttpPostedFileBase ProfilePic { get; set; }
        public  string  ProfilePicPath { get; set; }

        public int cityid { get; set; }
        public int countryid { get; set; }
        public string City { get; set; }

        public int ReqStatus { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }

    }
}
