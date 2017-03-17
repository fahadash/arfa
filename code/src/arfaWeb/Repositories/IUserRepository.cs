using arfaWeb.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Repositories
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
        void ChangePassword(string userName, string currentPassword, string newPassword);

        UserRepositoryOperationResult SignIn(string username, string password);

        User GetUser(string userId);

        void UpdateTokenTimestamp(int userId);

        IRegisterResult Register(string username, string password, string firstname, string lastname, int age);
    }
}
