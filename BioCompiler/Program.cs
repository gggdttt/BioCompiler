// Copyright (c) 2022 Wenjie Fan
// Department of Applied Mathematics
// Technical University of Denmark

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
                BioSyntaxBasicVisitor visitor = new BioSyntaxBasicVisitor();
                visitor.Visit(programContext);

                StringBuilder temp = new StringBuilder();

                foreach (var line in visitor.Lines)
                {
                    if (line is DropletCreator)
                    {
                        DropletCreator dropletCreator = (DropletCreator)line;
                        temp.AppendLine( JsonSerializer.Serialize(dropletCreator));
                        Console.WriteLine("droplet:{0},xValue:{1}, yValue:{2}" ,dropletCreator.name ,dropletCreator.xValue , dropletCreator.yValue);
                    }

                    else if (line is DropletSplitor)
                    {
                        DropletSplitor dropletSplitor = (DropletSplitor)line;
                        temp.AppendLine(JsonSerializer.Serialize(dropletSplitor));
                        Console.WriteLine("Splitor droplet Name:{2} to Name1:{0}, Name2:{1}",
                            dropletSplitor.droplet1Name,dropletSplitor.droplet2Name, dropletSplitor.originDropletName);
                    }

                    else if (line is MovingCreator)
                    {
                        MovingCreator movingCreator = (MovingCreator)line;
                        temp.AppendLine(JsonSerializer.Serialize(movingCreator));
                        Console.WriteLine("moving: Name{0} to x:{1} , y:{2}", movingCreator.name, movingCreator.xValue, movingCreator.yValue);
                    }

                }
                Console.WriteLine(temp);
                File.WriteAllText(@"C:\Users\Wenjie\OneDrive\MasterThesis\VisionBasedCompiler\BioCompiler\Source\Output\result.json", temp.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
