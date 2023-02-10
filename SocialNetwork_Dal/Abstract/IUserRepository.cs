using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_Dal.Abstract
{
    public interface IUserRepository
    {
        User GetUserData(string email);
        string CreatePost(Post post, int posterId);
    }
}
