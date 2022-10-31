// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace BioCompiler

{
    using System;
    using Antlr4.Runtime;
    using System.Text;
    using CommandLine;
    using static SyntaxParser;
    using System.IO;
    using BioCompiler.Compiler;
    using Executor.Model.Operation;
    using System.Text.Json;
    using Newtonsoft.Json;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                StringBuilder text = new StringBuilder();

                string path = Path.GetFullPath("C:\\Users\\Wenjie\\OneDrive\\MasterThesis\\VisionBasedCompiler\\BioCompiler\\Source\\InputProgram\\program1.sc");

                IEnumerable<string> fileContents = File.ReadAllLines(path);
                foreach (string s in fileContents)
                {
                    text.AppendLine(s);

                }

                AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
                SyntaxLexer speakLexer = new SyntaxLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
                SyntaxParser syntaxParser = new SyntaxParser(commonTokenStream);

                ProgramContext programContext = syntaxParser.program();
                BioOperationSyntaxBasicVisitor visitor = new BioOperationSyntaxBasicVisitor();
                visitor.Visit(programContext);

                StringBuilder temp = new StringBuilder();
                

                temp.Append(JsonConvert.SerializeObject(visitor.Lines, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));
               
                File.WriteAllText(@"C:\Users\Wenjie\OneDrive\MasterThesis\VisionBasedCompiler\BioCompiler\Source\Output\result.json", temp.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
