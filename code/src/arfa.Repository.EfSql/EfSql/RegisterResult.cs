﻿using arfa.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfa.Repository.EfSql
{
    public class RegisterResult : IRegisterResult
    {
        public UserRepositoryOperationResult Result { get; set ; }
        public int UserId { get; set; }
    }
}
