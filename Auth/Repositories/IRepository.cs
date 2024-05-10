using Auth.Models.User;

namespace Auth.Repositories
{
    public interface IRepository
    {
        List<UserDTO> Users { get; }
    }
}
