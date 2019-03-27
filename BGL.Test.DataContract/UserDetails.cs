using System.Collections.Generic;

namespace BGL.Test.DataContract
{
    public class UserDetails
    {
        //get the users name, location and avatar url from the returned json

        public string UserName { get; set; }

        public string Location { get; set; }

        public string AvatarUrl { get; set; }

        public string ReposUrl { get; set; }
        public IList<string> RepoList { get; set; }
    }
}
