using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Common.Enumeration
{
     public enum EStatusCodes
     {
            Continue = 100,
            Processing = 102,
            Ok = 200,
            Created = 201,
            NoContent = 204,
            BadRequest = 400,
            UnAuthorized = 401,
            Notfound = 404,
            NotAllowed = 405,
            alreadyExist = 409,
            Gone = 410,
     }
    
}
