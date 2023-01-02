using Executor.Model.Operation;
using System.Collections.Immutable;

namespace BioCompiler.Checker
{
    /// <summary>
    /// The checker is used to check if there are sytanx error in source code
    /// 
    /// </summary>
    interface Checker
    {
        public bool DoCheck(ImmutableArray<CompilerOperation> operations);
    }
}
