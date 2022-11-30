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
        public double volume { get; set; }
        public int gridDiameter { get; set; }

        // the gap between chip and glass is 0.1 mm.
        static readonly double H = 1;
        // the grid is 2mm * 2mm
        static readonly int GRID_LENGTH = 2;
        // redefine 
        public Droplet(string name, int xValue, int yValue, double size)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.volume = size;
            this.gridDiameter = RoundUpVolumeToGridDiameter(size);
        }

        public Droplet(string name)
        {
            this.name = name;
            this.xValue = 0;
            this.yValue = 0;
            this.volume = 0;
            this.gridDiameter = 0;
        }

        public override bool Equals(Object? obj)
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

        public override string ToString()
        {
            return "Droplet: " + name + " xValue:" + xValue + " yValue:" + yValue + " volume:" + volume;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        /// <summary>
        /// Translate the volume of the droplet from ml to how many grids it will occupy.
        /// </summary>
        /// <param name="volume">how many ml this droplet is </param>
        /// <returns></returns>
        private int RoundUpVolumeToGridDiameter(double volume)
        {

            // V = Pi* r^2 * h , convert it from cm to mm (10/2 =5)
            double radius = Math.Sqrt(volume / (H * Math.PI)) * (10 / GRID_LENGTH);
            Console.WriteLine((int)Math.Round(radius * 2, MidpointRounding.AwayFromZero));
            return (int)Math.Round(radius*2, MidpointRounding.AwayFromZero);
        }
    }
}
