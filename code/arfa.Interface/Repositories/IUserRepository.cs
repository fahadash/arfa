using arfaInterface.Models;
using arfaWeb.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaInterface.Repositories
{
    public enum UserRepositoryOperationResult
    {
        OperationSuccessful,
        InvalidUser,
        InvalidPassword,
        UsernameAlreadyInUse
    }

    public interface IUserRepository
    {
        User GetUser(Guid token);
        void ChangePassword(User user, string newPassword);

        User SignIn(string username, string password);

        User GetUser(int userId);

        void UpdateTokenTimestamp(int userId);

        IRegisterResult Register(string username, string password, string firstname, string lastname, int age);
    }
}
