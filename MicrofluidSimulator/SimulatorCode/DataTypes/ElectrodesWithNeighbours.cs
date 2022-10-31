// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace MicrofluidSimulator.SimulatorCode.DataTypes
{
    public class ElectrodesWithNeighbours
    {
        public string Name { get; set; }
        public int ID1 { get; set; }
        public int ElectrodeID { get; set; }
        public int DriverID { get; set; }
        public int Shape { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Status { get; set; }
        public List<List<int>> Corners { get; set; }
        public List<object> Subscriptions { get; set; }
        public List<int> Neighbours { get; set; }
    }
}
