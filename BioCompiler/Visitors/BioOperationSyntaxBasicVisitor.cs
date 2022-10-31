// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using static SyntaxParser;
using Executor.Model.Operation;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace BioCompiler.Compiler
{
    internal class BioOperationSyntaxBasicVisitor : SyntaxBaseVisitor<CompilerOperation>
    {
        public List<CompilerOperation> Lines = new List<CompilerOperation>();
        public override CompilerOperation VisitStat(StatContext context)
        {
            InputContext input = context.input();
            DeclarationContext declaration = context.declaration();
            MoveContext move = context.move();
            MergeContext merge = context.merge();
            SplitContext split = context.split();
            MixContext mix = context.mix();
            OutputContext output = context.output();
            StoreContext store = context.store();

            CompilerOperation? line = null;

            if (input != null)
            {
                line = new DropletInputer(input.ID().GetText()
                    , int.Parse(input.INT(0).GetText())
                    , int.Parse(input.INT(1).GetText())
                    , double.Parse(input.FLOAT().GetText()));
                Lines.Add(line);
            }
            else if (declaration != null)
            {
                line = new DropletDeclarator(declaration.ID().GetText());
                Lines.Add(line);
            }
            else if (move != null)
            {
                line = new DropletMover(move.ID().GetText()
                    , int.Parse(move.INT(0).GetText())
                    , int.Parse(move.INT(1).GetText()));
                Lines.Add(line);
            }
            else if (merge != null)
            {
                line = new DropletMerger(merge.ID(0).GetText()
                    , merge.ID(1).GetText()
                    , merge.ID(2).GetText()
                    , int.Parse(merge.INT(0).GetText())
                    , int.Parse(merge.INT(1).GetText()));
                Lines.Add(line);
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
                    , double.Parse(split.FLOAT().GetText()));
                Lines.Add(line);
            }
            else if (mix != null)
            {
                line = new DropletMixer(mix.ID().GetText()
                    , int.Parse(mix.INT(0).GetText())
                    , int.Parse(mix.INT(1).GetText())
                    , int.Parse(mix.INT(2).GetText())
                    , int.Parse(mix.INT(3).GetText())
                    , int.Parse(mix.INT(4).GetText()));
                Lines.Add(line);
            }
            else if (output != null)
            {
                line = new DropletOutputer(output.ID().GetText()
                    , int.Parse(output.INT(0).GetText())
                    , int.Parse(output.INT(1).GetText()));
                Lines.Add(line);
            }
            else if (store != null)
            {
                line = new DropletStorer(store.ID().GetText()
                    , int.Parse(store.INT(0).GetText())
                    , int.Parse(store.INT(1).GetText())
                    , double.Parse(store.FLOAT().GetText()));
                Lines.Add(line);
            }

            return line;
        }
    }
}
