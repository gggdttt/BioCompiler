// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using System.Text;

namespace ToolSupporter.FileOperator
{
    public class BioFileReader
    {
        public static string ReadFileAsString(string source)
        {
            StringBuilder text = new StringBuilder();

            string path = Path.GetFullPath(source);

            IEnumerable<string> fileContents = File.ReadAllLines(path);
            foreach (string s in fileContents)
            {
                text.AppendLine(s);
            }
            return text.ToString();
        }

    }
}
