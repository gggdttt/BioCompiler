// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

namespace Executor.Model
{
    public class Droplet
    {
        private readonly string name;
        private int xValue;
        private int yValue;
        private double size;
        private bool isBeingUsed;

        // redefine 
        public Droplet(string  name, int xValue, int yValue, double size, bool isBeingUsed)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.size = size;
            this.isBeingUsed = isBeingUsed;
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
