using Auth.Models.Membership;
using Auth.Repositories;

namespace Auth.Services
{
    public class MembershipService : IServices
    {
        private readonly IRepository _repository;
        public MembershipService(IRepository repository) 
        {
            _repository = repository;
        }
        public MembershipResult SignIn(SigninDTO signinDTO)
        {
           var user = _repository.Users.FirstOrDefault(user => user.Phone == "123" && user.Password == "password");
           return new MembershipResult { IsSuccess = user != null, ErrorMessage = user == null ? "Error": null};
        }

        public MembershipResult SignUp(SignupDTO signupDTO)
        {
            throw new NotImplementedException();
        }
    }
}
