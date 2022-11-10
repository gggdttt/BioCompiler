// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using Antlr4.Runtime;
using BioCompiler.Checker;
using BioCompiler.Compiler;
using Newtonsoft.Json;
using static SyntaxParser;
using System.Text;
using System.Collections.Immutable;
using ToolSupporter.FileOperator;

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
                string fileContent = BioFileReader.ReadFileAsString(option.Source!);

                BioOperationSyntaxBasicVisitor visitor = GetVisitor(fileContent);

                StringBuilder tempString = new StringBuilder();
                DropletDecAndOrderChecker checker = new DropletDecAndOrderChecker();
                checker.DoCheck(visitor.Lines.ToImmutableArray());

                tempString.Append(JsonConvert.SerializeObject(visitor.Lines, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));

                BioFileWriter.Write(option.Output!, tempString.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        private BioOperationSyntaxBasicVisitor GetVisitor(string str)
        {
            AntlrInputStream inputStream = new AntlrInputStream(str);
            SyntaxLexer speakLexer = new SyntaxLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            SyntaxParser syntaxParser = new SyntaxParser(commonTokenStream);

            ProgramContext programContext = syntaxParser.program();
            BioOperationSyntaxBasicVisitor visitor = new BioOperationSyntaxBasicVisitor();
            visitor.Visit(programContext);

            return visitor;
        }
    }

}
