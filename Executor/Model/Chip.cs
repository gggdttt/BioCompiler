using Executor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Model
{
    /// <summary>
    /// 
    /// </summary>
    internal class Chip
    {

        int xLength;

        int yLength;

        OperationManager manager;

        public Chip(ImmutableList<CompilerOperation> operations, int x, int y)
        {
            this.xLength = x;
            this.yLength = y;
            manager = new OperationManager(operations, x, y);
        }

        public void DoNextStep()
        {
            
        }

    }
}
