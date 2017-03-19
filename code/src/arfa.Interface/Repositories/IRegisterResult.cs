using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfa.Interface.Repositories
{
    public interface IRegisterResult
    {
        UserRepositoryOperationResult Result { get; set; }
        int UserId { get; set; }
    }
}
