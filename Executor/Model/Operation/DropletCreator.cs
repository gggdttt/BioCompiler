using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Model.Operation
{
    public class DropletCreator : CompilerOperation
    {
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }
        public int width { get; }
        public int length { get; }
        public int _order_id { get; }

        public Droplet execResult { get; set; }

        public Droplet Droplet { get; set; }
        public DropletCreator(string dropletName, int xValue, int yValue, int width, int length)
        {
            // TODO : add order_id here 
            this.name = dropletName;
            this.xValue = xValue;
            this.yValue = yValue;
            this.width = width;
            this.length = length;
        }



        public void ExecuteOperation()
        {
            this.execResult = new Droplet(name, xValue, yValue, width, length, false);
        }
    }
}
