using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using arfaWeb.Exception;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace arfaWeb.Controllers
{
    public class UserAccountController : Controller
    {
        object ResultObject(string errorCode, string errorMessage)
        {
            return new { errorcode = errorCode, errormessage = errorMessage };
        }

        object UserInfoObject(int userId, string userName, string firstName, string lastName, int age)
        {
            return new { userid = userId, username = userName, firstname = firstName, lastname = lastName, age = age };
        }

        public class LogInParams
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [HttpPost]
        public object LogIn(LogInParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ArfaException("Invalid model state");
            }
            object userLogin = null;
            try
            {
                userLogin = UserModel.LogIn(par.username, par.password);
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {
                        user = new { logintoken = Guid.Empty.ToString() }
                    }
                };
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = userLogin };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = userLogin };

        }

        public class RegisterParams
        {
            public string username { get; set; }
            public string password { get; set; }
            public string confirmpassword { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public int age { get; set; }
            public string reserved1 { get; set; }
            public string reserved2 { get; set; }
        }

        private void ValidateRegisterParams(RegisterParams param)
        {
            if (param.username.Length < 3)
            {
                throw new TCException("USERNAMETOOSHORT", "Username must be at least 3 characters long");
            }

            if (param.password != param.confirmpassword)
            {
                throw new TCException("PASSWORDMISMATCH", "Password and Confirm Password don't match");
            }

            if (param.password.Length < 5)
            {
                throw new TCException("PASSWORDTOOSHORT", "Password must be at least 4 characters long");
            }


            if (param.age < 14)
            {
                throw new TCException("USERTOOYOUNG", "Minimum age required is 14");
            }
        }

        [HttpPost]
        public object Register(RegisterParams registerParams)
        {
            if (!ModelState.IsValid)
            {
                throw new ArfaException("Invalid model state");
            }

            UserModel model;
            try
            {
                ValidateRegisterParams(registerParams);

                model = UserModel.CreateUser(registerParams.username, registerParams.password, registerParams.firstname,
                                     registerParams.lastname, registerParams.age);
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {
                        user = UserInfoObject(0, registerParams.username, registerParams.firstname, registerParams.lastname, registerParams.age)
                    }
                };
            }
            catch (Exception e)
            {
                return new
                {
                    result = ResultObject("FAILED", "Unknown exception occurred at controller: " + e.Message),
                    response = new
                    {
                        user = UserInfoObject(0, registerParams.username, registerParams.firstname, registerParams.lastname, registerParams.age)
                    }
                };
            }
            return new
            {
                result = ResultObject("SUCCESS", string.Empty),
                response = new
                {
                    user = UserInfoObject(0, model.UserName, model.Firstname, model.Lastname, model.Age)
                }
            };
        }

        public class ChangePasswordParams
        {
            public string username { get; set; }
            public string currentpassword { get; set; }
            public string newpassword { get; set; }
            public string confirmnewpassword { get; set; }
        }

        private void ValidateChangePasswordParams(ChangePasswordParams param)
        {

            if (param.newpassword != param.confirmnewpassword)
            {
                throw new TCException("PASSWORDMISMATCH", "New Password and Confirm New Password don't match");
            }

            if (param.newpassword.Length < 5)
            {
                throw new TCException("PASSWORDTOOSHORT", "New Password must be at least 5 characters long");
            }

        }
        [HttpPost]
        public object ChangePassword(ChangePasswordParams changePasswordParams)
        {
            if (!ModelState.IsValid)
            {
                throw new ArfaException("Invalid model state");
            }
            Guid token = Guid.Empty;
            try
            {
                ValidateChangePasswordParams(changePasswordParams);
                token = UserModel.ChangePassword(changePasswordParams.username, changePasswordParams.currentpassword, changePasswordParams.newpassword);
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = new { logintoken = token.ToString() } };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { logintoken = token.ToString() } };
        }
    }
}
