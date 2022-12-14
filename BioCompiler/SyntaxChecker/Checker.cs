using Executor.Model;
using Executor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
