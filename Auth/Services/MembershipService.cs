using Auth.Common;
using Auth.Common.Constants;
using Auth.Infrastructure;
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
            string errorMessage = string.Empty;
            bool isSuccess = true;
            try
            {
                string hashedPassword = Tools.HashPassword(signinDTO.Password);
                var user = _repository.Users.FirstOrDefault(user => user.Phone == signinDTO.Phone && user.Password == hashedPassword);

                if (user == null)
                {
                    isSuccess = false;
                    errorMessage = GetErrorMessage(ConstantsEnums.ErrorMessages.UserNotFound);
                }

                return new MembershipResult { IsSuccess = isSuccess, ErrorMessage = errorMessage };
            }
            catch (Exception ex)
            {
                return new MembershipResult { IsSuccess = isSuccess, ErrorMessage = GetErrorMessage(ConstantsEnums.ErrorMessages.InternalServerError) };
            }
        }

        public MembershipResult SignUp(SignupDTO signupDTO)
        {
            throw new NotImplementedException();
        }
        
        private string GetErrorMessage(ConstantsEnums.ErrorMessages messageKey)
        {
            return ResourceManagerSingleton.Instance.GetString(messageKey.ToString());
        }
    }
}
