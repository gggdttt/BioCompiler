using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Model.Operation
{
    public class MovingCreator: CompilerOperation
    {

        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }

        public MovingCreator(string name,  int xValue, int yValue)
        {

            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
        }


        public void ExecuteOperation()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
