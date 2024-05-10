using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth.Models.Membership;
using Auth.Services;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    // for jwt [Authorize]
    public class MembershipController : ControllerBase
    {
        private readonly IServices membershipService;

        public MembershipController(IServices membershipService)
        {
            this.membershipService = membershipService;
        }

        [HttpPost(Name = "Signup")]
        public IActionResult Signup([FromBody] SignupDTO signupDto)
        {
            return Ok(membershipService.SignUp(signupDto));
        }

        [HttpPost(Name = "Signin")]
        public IActionResult Signin([FromBody] SigninDTO signinDto)
        {
            return Ok(membershipService.SignIn(signinDto));
        }

        /*       

                [HttpPost(Name = "Signout")]
                public IActionResult Signout()
                {
                    return Ok(200);
                }

                [HttpPost(Name = "ForgotPassword")]
                public IActionResult ForgotPassword()
                {
                    return Ok(200);
                }*/
    }
}
