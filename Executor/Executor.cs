// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Text;
using System.Collections.Immutable;
using ToolSupporter.FileOperator;
using Executor.Model.Operation;
using Executor.Model.OperationHelper;
using Newtonsoft.Json;
using System;

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

                ImmutableArray<CompilerOperation> operations = 
                    JsonConvert.DeserializeObject<ImmutableArray<CompilerOperation>>(fileContent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Converters = converters });
                foreach (CompilerOperation operation in operations)
                {
                    Console.WriteLine(operation.ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                //TODO: create more exception
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }


    }

}
