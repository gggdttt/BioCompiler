// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using static SyntaxParser;
using Executor.Model.Operation;

namespace BioCompiler.Compiler
{
    internal class BioOperationSyntaxBasicVisitor : SyntaxBaseVisitor<CompilerOperation>
    {
        public List<CompilerOperation> Lines = new List<CompilerOperation>();
        public override CompilerOperation VisitStat(StatContext context)
        {
            DropletContext droplet = context.droplet();
            SplitingContext splitting = context.spliting();
            MovingContext moving = context.moving();

            CompilerOperation line = null;

            if (droplet != null)
            {
                line = new DropletCreator(droplet.ID().GetText()
                    , int.Parse(droplet.INT(0).GetText())
                    , int.Parse(droplet.INT(1).GetText())
                    , int.Parse(droplet.INT(2).GetText())
                    , int.Parse(droplet.INT(3).GetText()));

                Lines.Add(line);
            }
            if (splitting != null)
            {
                line = new DropletSplitor(splitting.ID(0).GetText()
                    , splitting.ID(1).GetText()
                    , splitting.ID(2).GetText()
                    , int.Parse(splitting.INT(0).GetText())
                    , int.Parse(splitting.INT(1).GetText())
                    , int.Parse(splitting.INT(2).GetText())
                    , int.Parse(splitting.INT(3).GetText()));

                Lines.Add(line);
            }

            if (moving != null)
            {
                line = new MovingCreator(moving.ID().GetText()
                    , int.Parse(moving.INT(0).GetText())
                    , int.Parse(moving.INT(1).GetText()));
                Lines.Add(line);
            }

            return line;
        }
    }
}
