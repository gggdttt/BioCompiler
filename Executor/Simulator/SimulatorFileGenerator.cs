using Executor.Model;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ToolSupporter.FileOperator;

[assembly: InternalsVisibleTo("MyCode.UnitTests")]
namespace Executor.Simulator
{
    public class SimulatorFileGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="routerResult"></param>
        public static void SimulatorBasmFileGenerator(string outPutpath, string templatePath, string routerResult)
        {
            string text = File.ReadAllText(templatePath);
            File.WriteAllText(outPutpath, text.Replace(".text", ".text\r\nmain:\r\n" + routerResult + "\r\n"));
        }

        public static void SimulatorJsonFileGenerator(string outPutpath, string templatePath, HashSet<Droplet> droplets, int width)
        {
            string text = File.ReadAllText(templatePath);
            text = text.Replace("\"droplets\": [],", $"\"droplets\": [{GetAllDropletInitInfo(droplets, width)}],");
            File.WriteAllText(outPutpath, text);
        }


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
