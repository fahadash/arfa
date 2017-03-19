
using arfa.Interface.Models;
using arfa.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace arfa.Interface.Business
{
    public interface IUserAccountService
    {
        Guid ChangePassword(Guid loginToken, string username, string currentPassword, string newPassword);

        User SignIn(string username, string password);

        IRegisterResult Register(string username, string password, string firstname, string lastname, int age);
    }
}
