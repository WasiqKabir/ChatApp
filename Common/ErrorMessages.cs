using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ErrorMessages
    {
        public static class Common
        {
            public static ErrorMessage UserNameIsTaken => new ErrorMessage("UserNameIsTaken", "Username is already taken.");
            public static ErrorMessage InvalidRequest => new ErrorMessage("InvalidRequest", "Request is invalid. Some fields are required");
            public static ErrorMessage UserNotFound => new ErrorMessage("InvalidUser", "There is no User with this UserName.");
        }

        public static class Validation
        {
            public static ErrorMessage FieldsAreRequired => new ErrorMessage("FieldsAreRequired", "Request is invalid. Username and Password is required.");
            public static ErrorMessage InvalidUser => new ErrorMessage("InvalidUser", "Either username or password is Invalid.");
            public static ErrorMessage MoreThanOneChatRecord => new ErrorMessage("MoreThanOneChatRecord", "Two users cannot have more than chat record");
        }
    }
}
