// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace MicrofluidSimulator.SimulatorCode.DataTypes
{
    public class SimulatorAction
    {
        //String actionName;
        //int actionOnID, actionChange;

        public SimulatorAction(string actionName, int actionOnID, int actionChange)
        {
            this.actionName = actionName;
            this.actionOnID = actionOnID;
            this.actionChange = actionChange;
        }

        public string actionName { get; set; }
        public int actionOnID { get; set; }
        public int actionChange { get; set; }
    }
}
