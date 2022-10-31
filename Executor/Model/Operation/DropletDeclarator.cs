// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

namespace Executor.Model.Operation
{
    /// <summary>
    /// droplet <name>;
    ///         string
    /// </summary>
    public class DropletDeclarator: CompilerOperation
    {

        public string name { get; }

        public DropletDeclarator(string name)
        {
            this.name = name;
        }


        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
