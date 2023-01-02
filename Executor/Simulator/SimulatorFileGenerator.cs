// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using Executor.Model;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MyCode.UnitTests")]
namespace Executor.Simulator
{
    public class SimulatorFileGenerator
    {
        /// <summary>
        /// Generate a Basm file according to a template.
        /// </summary>
        /// <param name="outPutpath">The path to Basm File</param>
        /// <param name="templatePath">The path of the Basm file template, used to replace content </param>
        /// <param name="routerResult">A string containing the path of route</param>
        public static void SimulatorBasmFileGenerator(string outPutpath, string templatePath, string routerResult)
        {
            string text = File.ReadAllText(templatePath);
            File.WriteAllText(outPutpath, text.Replace(".text", ".text\r\nmain:\r\n" + routerResult + "\r\n"));
        }

        /// <summary>
        /// Generate the simulator .json file. It contains all the config of the board and the initilization of droplets
        /// </summary>
        /// <param name="outPutpath">The output path of file</param>
        /// <param name="templatePath"> The path to the json template </param>
        /// <param name="droplets">The droplets will show</param>
        /// <param name="width"></param>
        public static void SimulatorJsonFileGenerator(string outPutpath, string templatePath, HashSet<Droplet> droplets, int width)
        {
            string text = File.ReadAllText(templatePath);
            text = text.Replace("\"droplets\": [],", $"\"droplets\": [{GetAllDropletInitInfo(droplets, width)}],");
            File.WriteAllText(outPutpath, text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droplets"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string GetAllDropletInitInfo(HashSet<Droplet> droplets, int width)
        {
            string result = string.Empty;
            foreach (Droplet droplet in droplets)
            {
                result += "\n\r{" + SimulatorDropletAdaptor.GetDropletJsonFormatSimulatorInfo(droplet, width) + "},";
            }
            return result.Substring(0, result.Length - 1);
        }
    }
}
