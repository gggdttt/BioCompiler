// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
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
        private float size;
        
        private bool isMoving;

        // redefine 
        public Droplet(string  name, int xValue, int yValue, float size, bool isMoving)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.size = size;
            this.isMoving = isMoving;
        }

    }
}
