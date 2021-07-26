using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.ServiceHelpers.Draws
{
    public static class UserServiceEndpoints
    {
        public const string AddUser = "User/AddUser";
        public const string ValidateUserCredentials = "User/ValidateUserCredentials?username={0}&password={1}";
    }
}
