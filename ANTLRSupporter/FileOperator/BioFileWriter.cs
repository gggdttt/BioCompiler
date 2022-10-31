// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSupporter.FileOperator
{
    internal class BioFileWriter
    {

        internal static void Write(string outputPath)
        {
            string fileName = Path.GetFileName(outputPath);

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.WriteLine("this is a test");
            }
        }

        /// <summary>
        /// This method is only for test, will be removed
        /// </summary>
        /// <param name="outputPath"></param>
        internal static void WriteSC(string outputPath)
        {
            string fileName = Path.GetFileName(outputPath);

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.WriteLine("Droplet d1 = Position(1,1)\r" +
                    "\n\r\nDroplet d2 = Position(1,2)\r" +
                    "\n\r\nDroplet d3 = Position(1,3)");
            }
        }
    }
}
