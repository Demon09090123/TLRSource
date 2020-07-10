using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Common
{
    public enum RegisterStatus
    {
        Success,
        UsernameTaken,
        InvalidUsername,
        InvalidPassword,
        TooManyRegisters
    }
}
