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
    public class DropletDeclarator : CompilerOperation
    {

        public string name { get; }
        public int line { get; }

        public DropletDeclarator(string name, int line)
        {
            this.name = name;
            this.line = line;
        }

        public int getLine()
        {
            return line;
        }
        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }

        /// <summary>
        /// If its name is not in declaredSet, add and return true
        /// If it has been included in any set, return false
        /// </summary>
        /// <param name="declaredSet">Declared Variables</param>
        /// <param name="occupiedSet">Variables are occupied</param>
        /// <returns> return true if it's added successfully, otherwise return false</returns>
        public bool DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {
            if (!declaredSet.Contains(name) && !occupiedSet.Contains(name))
            {
                declaredSet.Add(name);
                return true;
            }
            else return false;
        }

    }
}
