using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arfa.Interface.Repositories;

namespace arfa.Repository.EfSql
{
    public class UserRepository : IUserRepository
    {
        private readonly arfa.Repository.EfSql.Database.arfaDBContext dbContext;

        public UserRepository(arfa.Repository.EfSql.Database.arfaDBContext ctx)
        {
            dbContext = ctx;
        }

        public arfa.Interface.Models.User GetUser(Guid token)
        {
            return dbContext.User.Where(u => u.Token == token)
                .Select(user => user.ToInterface())
                .FirstOrDefault();
        }

        public void ChangePassword(arfa.Interface.Models.User user, string newPassword)
        {
            var dbUser = dbContext.User.First(u => u.UserId == user.UserId);
            dbUser.Password = newPassword;
            dbContext.SaveChanges();            
        }

        arfa.Interface.Models.User IUserRepository.GetUser(int userId)
        {
            return dbContext.User.Where(u => u.UserId == userId)
                .Select(user => user.ToInterface())
                .FirstOrDefault();
        }

        public IRegisterResult Register(string username, string password, string firstname, string lastname, int age)
        {
            if (dbContext.User.Any(u => u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new RegisterResult()
                {
                    Result = UserRepositoryOperationResult.UsernameAlreadyInUse
                };
            }

            var user = new arfa.Repository.EfSql.Database.User()
            {
                Username = username,
                Password = password,
                Firstname = firstname,
                Lastname = lastname,
                Age = age
            };

            dbContext.User.Add(user);

            dbContext.SaveChanges();

            return new RegisterResult()
            {
                Result = UserRepositoryOperationResult.OperationSuccessful,
                UserId = user.UserId
            };
        }

        public arfa.Interface.Models.User SignIn(string username, string password)
        {
            var user = dbContext.User
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                user.Token = Guid.NewGuid();
                user.TokenLastHit = DateTime.Now;

                dbContext.SaveChanges();
            }

            return user.ToInterface();
        }

        public void UpdateTokenTimestamp(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
