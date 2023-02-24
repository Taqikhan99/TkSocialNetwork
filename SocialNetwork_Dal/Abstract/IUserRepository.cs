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

        User GetProfileData(int id);
        bool UpdateProfile(User user);

        List<User> GetOtherUsers(int id);

        bool SendRequest(int from, int to);
        int GetFriendReqStatus(int toid, int fromid);
        List<FriendReq> GetFriendRequests(int id);
        bool acceptRequest(int fromid, int ofId);
        




    }

}
