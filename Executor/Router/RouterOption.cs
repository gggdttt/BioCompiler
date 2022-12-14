using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Router
{
    /// <summary>
    /// If you want to add a router, do not forget to add it to the enum
    /// </summary>
    public enum RouterOption
    {
        SimpleXY,
        AStar,
        ConflictBased
    }
}
