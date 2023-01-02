// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


namespace ToolSupporter.BioExceptions
{
    [Serializable]
    public class BioException : ApplicationException
    {
        private int code;
        private int line;
        public BioException()
        {
            code = 0;
            line = -1;
        }

        public BioException(int code, string msg)
            : base(msg)
        {
            this.code = code;
            line = -1;
        }

        public BioException(string message, int code, int line)
            : base($"in source file line {line}:{message}")
        {
            this.code = code;
            this.line = line;
        }

        public int GetCode()
        {
            return code;
        }

        public int GetLine()
        {
            return line;
        }
        public string GetMessage()
        {
            return $"exception at line{line} in source file:{base.Message}";
        }
    }
}
