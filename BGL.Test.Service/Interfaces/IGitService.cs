using BGL.Test.DataContract;

namespace BGL.Test.Service.Interfaces
{
    public interface IGitService
    {
        UserDetails GetUserDetails(string userName);
    }
}
