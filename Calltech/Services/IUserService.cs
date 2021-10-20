using Calltech.Models.Request;
using Calltech.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calltech.Services
{
   public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
