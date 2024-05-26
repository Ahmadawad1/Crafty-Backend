using Auth.Common;
using Auth.Common.Constants;
using Auth.Infrastructure;
using Auth.Models.Membership;
using Auth.Repositories;
using Auth.Models.User;

namespace Auth.Services
{
    public class MembershipService : IServices
    {
        private readonly IRepository _repository;
        private string errorMessage = string.Empty;
        private bool isSuccess = true;
        public MembershipService(IRepository repository) 
        {
            _repository = repository;
        }
        public MembershipResult SignIn(SigninDTO signinDTO)
        {
            try
            {
                string hashedPassword = Tools.HashPassword(signinDTO.Password);
                var user = _repository.GetUserByPhone(signinDTO.Phone);

                if (user == null)
                {
                    isSuccess = false;
                    errorMessage = GetErrorMessage(ConstantsEnums.ErrorMessages.UserNotFound);
                }
                else if (user.Result.Password != hashedPassword) 
                {
                    isSuccess = false;
                    errorMessage = GetErrorMessage(ConstantsEnums.ErrorMessages.WrongPassword);
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
            try
            {
                string hashedPassword = Tools.HashPassword(signupDTO.Password);
                string userId = Guid.NewGuid().ToString();
                var user = _repository.GetUserByPhone(signupDTO.Phone);


                if (user != null)
                {
                    isSuccess = false;
                    errorMessage = GetErrorMessage(ConstantsEnums.ErrorMessages.PhoneIsUsed);
                    return new MembershipResult { IsSuccess = isSuccess, ErrorMessage = errorMessage };
                }

                _repository.AddNewUser(new UserDTO { 
                    Id = userId,
                    Password = hashedPassword, 
                    FirstName = signupDTO.FirstName, 
                    LastName = signupDTO.LastName,
                    Phone = signupDTO.Phone
                });

                return new MembershipResult { IsSuccess = isSuccess, ErrorMessage = errorMessage };
            }
            catch (Exception ex)
            {
                return new MembershipResult { IsSuccess = false, ErrorMessage = GetErrorMessage(ConstantsEnums.ErrorMessages.InternalServerError) };
            }
        }
        
        private string GetErrorMessage(ConstantsEnums.ErrorMessages messageKey)
        {
            return ResourceManagerSingleton.Instance.GetString(messageKey.ToString());
        }
    }
}
