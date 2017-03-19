using arfa.Interface.Business;
using System;
using System.Collections.Generic;
using System.Text;
using arfa.Interface.Models;
using arfa.Interface.Exceptions;
using arfa.Interface.Repositories;

namespace arfa.Business.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository userRepository;

        public UserAccountService(IUserRepository repository)
        {
            userRepository = repository;
        }
        public Guid ChangePassword(Guid loginToken, string username, string currentPassword, string newPassword)
        {
            if (newPassword.Length < 5)
            {
                throw new ArfaException("PASSWORDTOOSHORT", "New Password must be at least 5 characters long");
            }

            var user = userRepository.GetUser(loginToken);
            if (user == null)
            {
                throw new ArfaException("INVALIDTOKEN", "Invalid token supplied. Make sure this user is logged in.");
            }
            userRepository.ChangePassword(user, newPassword);

            var login = userRepository.SignIn(username, newPassword);

            return login.Token;
        }

        public IRegisterResult Register(string username, string password, string firstname, string lastname, int age)
        {
            if (username.Length < 3)
            {
                throw new ArfaException("USERNAMETOOSHORT", "Username must be at least 3 characters long");
            }


            if (password.Length < 5)
            {
                throw new ArfaException("PASSWORDTOOSHORT", "Password must be at least 4 characters long");
            }


            if (age < 14)
            {
                throw new ArfaException("USERTOOYOUNG", "Minimum age required is 14");
            }

            var result = userRepository.Register(username, password, firstname, lastname, age);

            return result;
        }

        public User SignIn(string username, string password)
        {
            return userRepository.SignIn(username, password);
        }
    }
}
