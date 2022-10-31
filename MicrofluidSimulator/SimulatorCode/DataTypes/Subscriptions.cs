// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace MicrofluidSimulator.SimulatorCode.DataTypes
{
    public class Subscriptions
    {
        //int subscriptionType; 

        public Subscriptions(int subscription)
        {
            this.subscriptionType = subscriptionType;
        }

        public Subscriptions()
        {
            this.subscriptionType = -1; //null
        }

        public int subscriptionType { get; set; }
    }
}
