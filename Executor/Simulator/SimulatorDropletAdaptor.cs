using Executor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Simulator
{
    public class SimulatorDropletAdaptor
    {
        /// <summary>
        /// This is used to record how many droplets there will be.
        /// </summary>
        static int DropletId = 0;

        /// <summary>
        /// Generate the droplets information for simulator's json file
        /// </summary>
        /// <param name="droplet">The droplets occur in this running</param>
        /// <param name="width"> The width of the board(only need width to calculate the index of cell)</param>
        /// <returns> A string in json format </returns>
        public static string GetDropletJsonFormatSimulatorInfo(Droplet droplet, int width)
        {
            return $" \"name\": \"{droplet.name}\",\r\n" +
                $"\"ID\": {DropletId++},\r\n" +
                "\"substance_name\": \"h20\",\r\n" +
                $"\"positionX\": {110 + droplet.xValue * 20 + droplet.gridDiameter * 20/2},\r\n" +
                $"\"positionY\": {droplet.yValue * 20 + droplet.gridDiameter * 20 / 2},\r\n" +
                $"\"sizeX\": {droplet.gridDiameter * 20},\r\n" +
                $"\"sizeY\": {droplet.gridDiameter * 20},\r\n" +
                "\"color\": \"#FFB00B\",\r\n" +
                "\"temperature\": 20,\r\n" +
                $"\"electrodeID\": {droplet.xValue+1 + width * droplet.yValue}";
        }
    }
}
