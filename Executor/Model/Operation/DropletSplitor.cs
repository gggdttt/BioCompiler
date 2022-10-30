using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Executor.Model.Operation
{
    public class DropletSplitor: CompilerOperation
    {
        public string originDropletName { get; }
        public string droplet1Name { get; }
        public int droplet1XValue { get; }
        public int droplet1YValue { get; }
        public int droplet1Width { get; }
        public int droplet1Length { get; }

        public string droplet2Name { get; }
        public int droplet2XValue { get; }
        public int droplet2YValue { get; }
        public int droplet2Width { get; }
        public int droplet2Length { get; }
        public int _order_id { get; }

        public Droplet result1 { get; set; }
        public Droplet result2 { get; set; }
        public DropletSplitor(string originDropletName, string aimDroplet1, string aimDroplet2 , int xValue1, int yValue1, int xValue2, int yValue2)
        {
            // TODO : add order_id here 
            this.droplet1Name = aimDroplet1;
            this.droplet1XValue = xValue1;
            this.droplet1YValue = yValue1;

            this.droplet2Name = aimDroplet2;
            this.droplet2XValue = xValue2;
            this.droplet2YValue = yValue2;
            this.originDropletName = originDropletName;
        }

        public void ExecuteOperation()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
