// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

namespace Executor.Model
{
    public class Droplet
    {
        public string name { get; set; }
        public int xValue { get; set; }
        public int yValue { get; set; }
        public double size { get; set; }

        // redefine 
        public Droplet(string name, int xValue, int yValue, double size)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.size = size;
        }

        public Droplet(string name)
        {
            this.name = name;
            this.xValue = 0;
            this.yValue = 0;
            this.size = 0;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Droplet d = (Droplet)obj;
                return name == d.name;
            }
        }
    }
}
