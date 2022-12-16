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
        public static void SimulatorBasmFileGenerator(string path, string routerResult)
        {
            string data = ".data\r\nDEF zero_addr 0\r\nzero: INT zero_addr 0\r\n";
            string text = ".text\r\nmain:\r\n" + routerResult + "\r\n";
            string config = "# Bio-Virtual-Machine configuration\r\n.configuration\r\ndata_memory_size -1\r\nprivate_data_memory_start_address 1000\r\ndebug_log_data_types_for_print_formatting true\r\ndebug_keep_all_private_data_memories false";
            BioFileWriter.Write(path, data + text + config);
        }

        public static void SimulatorJsonFileGenerator(string outPutpath, string templatePath, HashSet<Droplet> droplets, int width)
        {
            string text = File.ReadAllText(templatePath);
            text = text.Replace("\"droplets\": [],", $"\"droplets\": [{GetAllDropletInitInfo(droplets, width)}],");
            Console.WriteLine(GetAllDropletInitInfo(droplets, width));
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
