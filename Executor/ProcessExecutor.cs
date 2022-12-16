// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using ToolSupporter.FileOperator;
using Executor.Model.Operation;
using Executor.Model.OperationHelper;
using Newtonsoft.Json;
using Executor.Model;
using Executor.Simulator;

namespace Executor

{
    public class ProcessExecutor
    {
        ExecutorLaunchOption option;

        public ProcessExecutor()
        {
            option = new ExecutorLaunchOption();
        }

        public ProcessExecutor(ExecutorLaunchOption option)
        {
            this.option = option;
        }

        public int Execute()
        {
            try
            {
                BioExecutorConfig bioExecutorConfig = ConfigReader.GetConfig(option.Config!).bioExecutorConfig;

                // start to read source file
                string fileContent = BioFileReader.ReadFileAsString(option.Source!);
                List<CompilerOperation> operations = GetOperationsListFromJSON(fileContent);

                WriteOriginPositionToJsonSimulator(operations, bioExecutorConfig.output, bioExecutorConfig.template, bioExecutorConfig.column);

                Chip c = new Chip(operations, bioExecutorConfig.column, bioExecutorConfig.row, bioExecutorConfig.router);
                c.StartOpearions();
                return 0;
            }
            catch (Exception ex)
            {
                //TODO: create more exception
                Console.WriteLine(ex.StackTrace);
                return -1;
            }
        }

        public List<CompilerOperation> GetOperationsListFromJSON(string fileContent)
        {
            JsonConverter[] converters = { new CompilerOperation2JSONConverter() };
            return JsonConvert.DeserializeObject<List<CompilerOperation>>(fileContent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Converters = converters })!;
        }

        private void WriteOriginPositionToJsonSimulator(List<CompilerOperation> operations, string outputPath, string templatePath, int width)
        {
            HashSet<Droplet> dropletsBeforeExecuting = new HashSet<Droplet>();
            foreach (CompilerOperation operation in operations)
            {
                if (operation is DropletInputer)
                {

                    DropletInputer inputer = (DropletInputer)operation;
                    Droplet d = new Droplet(inputer.name, inputer.xValue, inputer.yValue, inputer.size);
                    if (!dropletsBeforeExecuting.Contains(d))
                        dropletsBeforeExecuting.Add(d);
                }
            }
            SimulatorFileGenerator.SimulatorJsonFileGenerator(outputPath, templatePath, dropletsBeforeExecuting, width);

        }
    }

}
