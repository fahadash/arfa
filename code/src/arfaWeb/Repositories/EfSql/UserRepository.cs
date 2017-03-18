using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arfaWeb.Database;

namespace arfaWeb.Repositories.EfSql
{
    public class UserRepository : IUserRepository
    {
        private readonly arfaDBContext dbContext;

        public UserRepository(arfaDBContext ctx)
        {
            dbContext = ctx;
        }

        public User GetUser(Guid token)
        {
            return dbContext.User.Where(u => u.Token == token).FirstOrDefault();
        }

        void IUserRepository.ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;
            dbContext.SaveChanges();
        }

        User IUserRepository.GetUser(int userId)
        {
            return dbContext.User.Where(u => u.UserId == userId).FirstOrDefault();
        }

        IRegisterResult IUserRepository.Register(string username, string password, string firstname, string lastname, int age)
        {
            throw new NotImplementedException();
        }

        User IUserRepository.SignIn(string username, string password)
        {
            var user = dbContext.User
                .FirstOrDefault(u => u.Username == username && u.Password == u.Password);

            if (user != null)
            {
                user.Token = Guid.NewGuid();
                user.TokenLastHit = DateTime.Now;

                dbContext.SaveChanges();
            }

            return user;
        }

        void IUserRepository.UpdateTokenTimestamp(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
