// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using Antlr4.Runtime;
using BioCompiler.Checker;
using BioCompiler.Compiler;
using CommandLine;
using Newtonsoft.Json;
using static SyntaxParser;
using System.Text;
using System.Collections.Immutable;

namespace BioCompiler

{
    internal class Runner
    {
        Option option;

        public Runner(Option option)
        {
            this.option = option;
        }

        public int run()
        {
            try
            {
                // start to read source file
                StringBuilder text = new StringBuilder();

                /*                string path = Path.GetFullPath("C:\\Users\\Wenjie\\OneDrive\\MasterThesis\\VisionBasedCompiler\\BioCompiler\\Source\\InputProgram\\error1.sc");
                */
                string path = Path.GetFullPath(option.Source!);

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
                DropletDecAndOrderChecker checker = new DropletDecAndOrderChecker();
                checker.DoCheck(visitor.Lines.ToImmutableArray());

                temp.Append(JsonConvert.SerializeObject(visitor.Lines, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));

                string outPutPath = Path.GetFullPath(option.Output!);
                File.WriteAllText(outPutPath, temp.ToString());

                return 0;
                //File.WriteAllText(@"C:\Users\Wenjie\OneDrive\MasterThesis\VisionBasedCompiler\BioCompiler\Source\Output\result.json", temp.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }
    }

}
