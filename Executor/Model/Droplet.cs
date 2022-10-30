using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Model
{
    public class Droplet
    {
        private readonly string name;
        private int xValue;
        private int yValue;
        private int width;
        private int length;
        private bool isMoving;

        // redefine 
        public Droplet(string  name, int xValue, int yValue, int width, int length, bool isMoving)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.width = width; 
            this.length = length;
            this.isMoving = isMoving;
        }


    }
}
