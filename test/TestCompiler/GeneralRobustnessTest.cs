// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler
{
    [TestClass]
    public class GeneralRobustnessTest
    {

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for REPEAT                                        //
        //                                                                                           //
        //===========================================================================================//
        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void TestCheckRepeatWithUndeclaredDroplets()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d1,5,5);\r\n" +
                "move(d2,10,10);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n\r\n";
            new Runner().DoCompile(origin);
        }


        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void TestCheckRepeatError2()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d3,7,7);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n\r\n";
            new Runner().DoCompile(origin);
        }

        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void TestCheckRepeatError3()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d3,0,0);\r\n\r\n";
            new Runner().DoCompile(origin);
        }
    }
}