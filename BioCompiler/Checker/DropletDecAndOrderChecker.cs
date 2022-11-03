using Executor.Model;
using Executor.Model.Operation;
using System.Collections.Immutable;

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
                if (!operation.DeclarationCheck(declaredSet, occupiedSet))
                {
                    /*                    Console.WriteLine("declaredSet"+string.Join("", declaredSet.ToArray()));
                                        Console.WriteLine("occupiedSet:"+string.Join("", occupiedSet.ToArray()));*/
                    // TODO, throw Exception formally
                    throw new Exception("Calling an undeclared droplet or this droplet is occupied in line:" + operation.getLine());
                }
            }
            return true;
        }


    }
}
