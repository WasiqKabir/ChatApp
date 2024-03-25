using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public ErrorMessage ErrorMessage { get; set; }
        public BusinessException()
            : base()
        {

        }
        public BusinessException(string code, string message)
            : base(message)
        {
            ErrorMessage = new ErrorMessage(code, message);
        }
        public BusinessException(ErrorMessage errorMessage)
           : base(errorMessage.Message)
        {
            ErrorMessage = errorMessage;
        }
    }
}
