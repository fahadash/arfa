using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC2.Common.Exceptions;

namespace TC2.Models
{
    using TCService2._2.Database;

    public class AccountModel
    {

    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public int Age { get; set; }


        public static UserModel Load(int userId)
        {

            return new UserModel()
            {
                UserId = 0,
                UserName = "abc",
                Firstname = "first",
                Lastname = "last",
                Age = 0
            };
        }

        public static UserModel Load(string userName, string password)
        {
            return new UserModel()
            {
                UserId = 0,
                UserName = "abc",
                Firstname = "first",
                Lastname = "last",
                Age = 0
            };
        }

        public static object LogIn(string userName, string password)
        {
            using (TC2DataContext ctx =  new TC2DataContext())
            {
                TC2DataContext ctxx = ctx;
                var results = ctxx.LogIn(userName, password);
                usp_LogInResult result = results.FirstOrDefault();

                if (!result.LoginToken.HasValue || result.LoginToken.Value == Guid.Empty || result.ErrorCode != "SUCCESS")
                {
                    throw new TCException(result.ErrorCode, result.Message);
                }

                return new { logintoken = result.LoginToken.Value, firstname = result.Firstname, lastname = result.Lastname };
            }
        }

        public static Guid ChangePassword(string userName, string currentPassword, string newPassword)
        {
            using (TC2DataContext ctx = new TC2DataContext())
            {
                TC2DataContext ctxx = ctx;
                var results = ctxx.ChangePassword(userName, currentPassword, newPassword);
                usp_ChangePasswordResult result = results.FirstOrDefault();

                if (!result.LoginToken.HasValue || result.LoginToken.Value == Guid.Empty || result.ErrorCode != "SUCCESS")
                {
                    throw new TCException(result.ErrorCode, result.Message);
                }

                return result.LoginToken.Value;
            }
        }


        public static UserModel CreateUser(string userName, string password, string firstName, string lastName, int age)
        {
            using (TC2DataContext ctx =  new TC2DataContext())
            {
                TC2DataContext ctxx = ctx;
                var results = ctxx.RegisterUser(userName, password, firstName, lastName, age);


                usp_RegisterUserResult result = results.FirstOrDefault();

                if (!result.UserId.HasValue || result.UserId.Value == 0)
                {
                    throw new TCException(result.ErrorCode, result.Message);
                }

                return new UserModel()
                {
                    UserId = result.UserId.HasValue?result.UserId.Value:0,
                    UserName = userName,
                    Firstname = firstName,
                    Lastname = lastName,
                    Age = age
                };
            }

        }

        public static UserModel GetUserByToken(Guid token)
        {
            TC2DataContext ctx = new TC2DataContext();
            
            var query = from user in ctx.Users
                        where user.Token.Equals(token) && user.TokenLastHit.HasValue
                        select user;

            User u = query.FirstOrDefault();
            

            if (u != null)
            {
                ctx.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, u);
                TimeSpan time =  DateTime.Now.Subtract(u.TokenLastHit.Value);

                if (time.TotalMinutes <= 15)
                {
                    u.TokenLastHit = DateTime.Now;
                    ctx.SubmitChanges();
                    return new UserModel
                    {
                        UserId = u.UserId,
                        UserName = u.Username,
                        Firstname = u.Firstname,
                        Lastname = u.Lastname,
                        Age = u.Age.HasValue?u.Age.Value : 0
                    };
                    
                }
            }

            return null;
        }
    }
}