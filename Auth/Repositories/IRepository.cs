using Auth.Models.Membership;
using Auth.Models.User;

namespace Auth.Repositories
{
    public interface IRepository
    {
       Task<UserDTO> AddNewUser(UserDTO user);
       Task<UserDTO> GetUserByPhone(string phone);
    }
}
