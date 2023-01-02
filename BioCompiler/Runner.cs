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
using ToolSupporter.BioExceptions;

namespace BioCompiler

{
    public class Runner
    {
        CompilerLaunchOption option;

        public Runner()
        {
            option = new CompilerLaunchOption();
        }

        public Runner(CompilerLaunchOption option)
        {
            this.option = option;
        }

        public int Run()
        {
            try
            {
                // start to read source file
                string fileContent = BioFileReader.ReadFileAsString(option.Source!);
                // start to compile
                string result = DoCompile(fileContent);
                // write result to file 
                BioFileWriter.Write(option.Output!, result);
                return 0;
            }
            catch (BioException ex)
            {
                Console.WriteLine("Compiler Error: " + ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -2;
            }
        }


        public string DoCompile(string fileContent)
        {

            try
            {
                BioOperationSyntaxBasicVisitor visitor = GetVisitor(fileContent);

                StringBuilder tempString = new StringBuilder();
                DropletDecAndOrderChecker checker = new DropletDecAndOrderChecker();

                checker.DoCheck(visitor.Lines.ToImmutableArray());

                tempString.Append(JsonConvert.SerializeObject(visitor.Lines, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));

                return tempString.ToString();
            }
            catch(NullReferenceException ex)
            {
                throw new IncorrectSyntaxException(ex.Message);
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
