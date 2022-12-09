// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using static SyntaxParser;
using Executor.Model.Operation;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
using System.Collections.Generic;

namespace BioCompiler.Compiler
{
    public class BioOperationSyntaxBasicVisitor : SyntaxBaseVisitor<CompilerOperation>
    {
        public List<CompilerOperation> Lines = new List<CompilerOperation>();

        public override CompilerOperation VisitStat(StatContext context)
        {
            return VisitStatByLevel(context, Lines);
        }
        private CompilerOperation VisitStatByLevel(StatContext context, List<CompilerOperation> tempLines)
        {
            InputContext input = context.input();
            DeclarationContext declaration = context.declaration();
            MoveContext move = context.move();
            MergeContext merge = context.merge();
            SplitContext split = context.split();
            MixContext mix = context.mix();
            OutputContext output = context.output();
            StoreContext store = context.store();
            RepeatContext repeat = context.repeat();

            CompilerOperation? line = null;

            if (input != null)
            {
                line = new DropletInputer(input.ID().GetText()
                    , int.Parse(input.INT(0).GetText())
                    , int.Parse(input.INT(1).GetText())
                    , double.Parse(input.FLOAT().GetText())
                    , input.Start.Line);
            }
            else if (declaration != null)
            {
                line = new DropletDeclarator(declaration.ID().GetText(), declaration.Start.Line);
            }
            else if (move != null)
            {
                line = new DropletMover(move.ID().GetText()
                    , int.Parse(move.INT(0).GetText())
                    , int.Parse(move.INT(1).GetText())
                    , move.Start.Line);
            }
            else if (merge != null)
            {
                line = new DropletMerger(merge.ID(0).GetText()
                    , merge.ID(1).GetText()
                    , merge.ID(2).GetText()
                    , int.Parse(merge.INT(0).GetText())
                    , int.Parse(merge.INT(1).GetText())
                    , merge.Start.Line);
            }
            else if (split != null)
            {
                line = new DropletSplitter(split.ID(0).GetText()
                    , split.ID(1).GetText()
                    , split.ID(2).GetText()
                    , int.Parse(split.INT(0).GetText())
                    , int.Parse(split.INT(1).GetText())
                    , int.Parse(split.INT(2).GetText())
                    , int.Parse(split.INT(3).GetText())
                    , double.Parse(split.FLOAT().GetText())
                    , split.Start.Line);
            }
            else if (mix != null)
            {
                line = new DropletMixer(mix.ID().GetText()
                    , int.Parse(mix.INT(0).GetText())
                    , int.Parse(mix.INT(1).GetText())
                    , int.Parse(mix.INT(2).GetText())
                    , int.Parse(mix.INT(3).GetText())
                    , int.Parse(mix.INT(4).GetText())
                    , mix.Start.Line);
            }
            else if (output != null)
            {
                line = new DropletOutputer(output.ID().GetText()
                    , int.Parse(output.INT(0).GetText())
                    , int.Parse(output.INT(1).GetText())
                    , output.Start.Line);
            }
            else if (store != null)
            {
                line = new DropletStorer(store.ID().GetText()
                    , int.Parse(store.INT(0).GetText())
                    , int.Parse(store.INT(1).GetText())
                    , double.Parse(store.FLOAT().GetText())
                    , store.Start.Line);
            }
            else if(repeat != null)
            {
                List<CompilerOperation> repeatOperations = new List<CompilerOperation>();
                foreach (StatContext child in repeat.stat())
                {
                    // recursively add operations to list
                    repeatOperations.Add(VisitStatByLevel(child, new List<CompilerOperation>()));
                }
                line = new RepeatOperation(repeat.Start.Line
                    , int.Parse(repeat.INT().GetText())
                    , repeatOperations); 
            }
            tempLines.Add(line);
            return line;
        }
    }
}
