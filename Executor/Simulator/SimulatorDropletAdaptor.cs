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
        static int i = 0;
        public static string GetDropletJsonFormatSimulatorInfo(Droplet droplet, int width)
        {
            return $" \"name\": \"{droplet.name}\",\r\n" +
                $"\"ID\": {i++},\r\n" +
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
