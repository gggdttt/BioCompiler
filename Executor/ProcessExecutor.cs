// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using ToolSupporter.FileOperator;
using Executor.Model.Operation;
using Executor.Model.OperationHelper;
using Newtonsoft.Json;
using Executor.Model;

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
                // start to read source file
                string fileContent = BioFileReader.ReadFileAsString(option.Source!);
                Chip c = new Chip(GetOperationsListFromJSON(fileContent), 20, 20);
                c.DoNextStep();
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


    }

}
