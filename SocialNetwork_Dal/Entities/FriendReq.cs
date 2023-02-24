using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_Dal.Entities
{
    public class FriendReq
    {
        public int RequesterId { get; set; }

        public string RequesterName { get; set; }

        public string RequesterPic { get; set; }

        public DateTime RequestTime { get; set; }


    }
}
