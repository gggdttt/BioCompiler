// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace BioCompilerTest
{
    [TestClass]
    public class DropletDeclarationTest
    {
        public string origin = "# this is a demo\r\n" +
            "# droplet declaration\r\n" +
            "droplet d1;\r\n" +
            "droplet d2;\r\n" +
            "droplet d3;\r\n";

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}