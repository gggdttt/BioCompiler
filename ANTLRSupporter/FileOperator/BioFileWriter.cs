// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using System.Text;

namespace ToolSupporter.FileOperator
{
    public class BioFileWriter
    {

        public static void Write(string outputPath, string content)
        {

            string outPutPath = Path.GetFullPath(outputPath);
            File.WriteAllText(outPutPath, content);
        }

    }
}
