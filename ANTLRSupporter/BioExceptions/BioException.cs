using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ToolSupporter.BioExceptions
{
    [Serializable]
    public class BioException : ApplicationException
    {
        private int code;
        public BioException()
        {
            code = 0;
        }
        public BioException(string message, int code)
            : base(message)
        {
            this.code = code;
        }

        public int GetCode()
        {
            return code;
        }

        public string GetMessage()
        {
            return base.Message;
        }
    }
}
