using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using arfaWeb.Exceptions;
using arfaWeb.Repositories;
using arfaWeb.Models.Requests;
using arfaWeb.Models.Response;
using arfaWeb.Helpers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace arfaWeb.Controllers
{
    [Route("api/v2/[controller]/[action]")]
    public class UserAccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserAccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = userRepository.SignIn(request.username, request.password);
            
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
                username = user.Username
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
                if (request.username.Length < 3)
                {
                    throw new ArfaException("USERNAMETOOSHORT", "Username must be at least 3 characters long");
                }

                if (request.password != request.confirmPassword)
                {
                    throw new ArfaException("PASSWORDMISMATCH", "Password and Confirm Password don't match");
                }

                if (request.password.Length < 5)
                {
                    throw new ArfaException("PASSWORDTOOSHORT", "Password must be at least 4 characters long");
                }


                if (request.age < 14)
                {
                    throw new ArfaException("USERTOOYOUNG", "Minimum age required is 14");
                }
                #endregion

                var result = userRepository.Register(request.username, request.password, request.firstName, request.lastName,
                        request.age);
                var login = userRepository.SignIn(request.username, request.password);

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

                if (request.newPassword.Length < 5)
                {
                    throw new ArfaException("PASSWORDTOOSHORT", "New Password must be at least 5 characters long");
                }
                #endregion

                var user = userRepository.GetUser(request.loginToken);
                userRepository.ChangePassword(user, request.newPassword);

                var login = userRepository.SignIn(request.username, request.newPassword);

                return new ChangePasswordResponse()
                {
                    status = "SUCCESS",
                    message = "Password changed successfully",
                    token  = login.Token.ToString(),
                    username = user.Username
                };
            }
            catch (Exception e)
            {
                return ExceptionHelper.CreateResponse<ChangePasswordResponse>(e);
            }

        }
    }
}
