// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Text;
using ToolSupporter.FileOperator;
using Executor.Model.Operation;
using Executor.Model.OperationHelper;
using Newtonsoft.Json;
using System;
using Executor.Model;

namespace Executor

{
    public class Executor
    {
        ExecutorLaunchOption option;

        public Executor()
        {
            option = new ExecutorLaunchOption();
        }

        public Executor(ExecutorLaunchOption option)
        {
            this.option = option;
        }

        public int Execute()
        {
            try
            {
                // start to read source file
                string fileContent = BioFileReader.ReadFileAsString(option.Source!);

                JsonConverter[] converters = { new CompilerOperation2JSONConverter() };

                List<CompilerOperation> operations = JsonConvert.DeserializeObject<List<CompilerOperation>>(fileContent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Converters = converters })!;
                foreach (CompilerOperation operation in operations)
                {
                    Console.WriteLine(operation.ToString());
                }

                Chip c = new Chip(operations, 20, 20);
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


    }

}
