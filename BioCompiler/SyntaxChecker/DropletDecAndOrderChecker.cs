using Executor.Model;
using Executor.Model.Operation;
using System.Collections.Immutable;
using ToolSupporter.BioExceptions;

namespace BioCompiler.Checker
{
    /// <summary>
    /// The checker is used to check 
    /// 1. If the droplet is declared before using
    /// 2. If there are the same names for different droplet
    /// </summary>
    class DropletDecAndOrderChecker : Checker
    {
        public bool DoCheck(ImmutableArray<CompilerOperation> operations)
        {
            HashSet<string> declaredSet = new HashSet<string>();
            HashSet<string> occupiedSet = new HashSet<string>();

            foreach (CompilerOperation operation in operations)
            {

                operation.DeclarationCheck(declaredSet, occupiedSet);
                   // throw new DropletDeclaredMoreThanOnceException("Calling an undeclared droplet or this droplet is occupied in line:" + operation.GetLine());
            }
            return true;
        }


    }
}
