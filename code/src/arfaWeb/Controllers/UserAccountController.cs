using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using arfaWeb.Models.Requests;
using arfaWeb.Models.Response;
using arfaWeb.Helpers;
using arfa.Interface.Business;
using arfa.Interface.Exceptions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace arfaWeb.Controllers
{
    [Route("api/v2/[controller]/[action]")]
    public class UserAccountController : Controller
    {
        private readonly IUserAccountService service;

        public UserAccountController(IUserAccountService svc)
        {
            this.service = svc;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = service.SignIn(request.username, request.password);
            
            if (user == null)
            {
                return StatusCode(403, new Response()
                {
                    status = "FAILED",
                    message = "Login failed"
                });
            }

            return Ok(new LoginResponse()
            {
                status = "SUCCESS",
                message = "Login Successful",
                token = user.Token.ToString(),
                username = user.UserName
            });
        }

        [HttpPost]
        public RegisterResponse Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ArfaException("Invalid model state");
            }

            try
            {
                #region Validate Params
                if (request.password != request.confirmPassword)
                {
                    throw new ArfaException("PASSWORDMISMATCH", "Password and Confirm Password don't match");
                }

                #endregion

                var result = service.Register(request.username, request.password, request.firstName, request.lastName,
                        request.age);

                if (result.Result == arfa.Interface.Repositories.UserRepositoryOperationResult.UsernameAlreadyInUse)
                {
                    throw new ArfaException("USERNAMETAKEN", "This username is already taken, please choose another");
                }

                var login = service.SignIn(request.username, request.password);

                return new RegisterResponse()
                {
                    status = "SUCCESS",
                    message = "User registered successfully",
                    username = request.username,
                    token = login.Token.ToString()
                };
            }
            catch (Exception e)
            {
                return ExceptionHelper.CreateResponse<RegisterResponse>(e);
            }
            return null;
        }
        
        [HttpPost]
        public ChangePasswordResponse ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ArfaException("Invalid model state");
            }
            Guid token = Guid.Empty;
            try
            {
                #region Validate Params
                if (request.newPassword != request.confirmPassword)
                {
                    throw new ArfaException("PASSWORDMISMATCH", "New Password and Confirm New Password don't match");
                }
                #endregion

                var newtoken = service.ChangePassword(request.loginToken, request.username, request.currentPassword, request.newPassword);

                return new ChangePasswordResponse()
                {
                    status = "SUCCESS",
                    message = "Password changed successfully",
                    token  = newtoken.ToString(),
                    username = request.username
                };
            }
            catch (Exception e)
            {
                return ExceptionHelper.CreateResponse<ChangePasswordResponse>(e);
            }

        }
    }
}
