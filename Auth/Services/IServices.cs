using Auth.Models.Membership;

namespace Auth.Services
{
    public interface IServices
    {
        MembershipResult SignUp(SignupDTO signupDTO);
        MembershipResult SignIn(SigninDTO signinDTO);
    }
}
