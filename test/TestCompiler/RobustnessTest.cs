using System.Data;
using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler
{
    [TestClass]
    public class RobustnessTest
    {


        //===========================================================================================//
        //                                                                                           //
        //                                    Test for COMMEN  OPERATION                             //
        //                                                                                           //
        //===========================================================================================//

        //========================================
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclaredTest1()
        {
            string origin = 
                "droplet d1;" +
                "droplet d2;" +
                "input(d3,10,10,3.2);";
            new Runner().DoCompile(origin);
        }

        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclaredTest2()
        {
            string origin =
                "input(d1000,10,10,3.2);";
            new Runner().DoCompile(origin);
        }
        //===========================================

        //===========================================
        [TestMethod]
        [ExpectedException(typeof(DropletDeclaredMoreThanOnceException))]
        public void DropletDeclaredMoreThanOnceTest()
        {
            string origin =
                "droplet d1;" +
                "droplet d1;";
            new Runner().DoCompile(origin);
        }
        //===========================================

        //===========================================
        [TestMethod]
        public void IncorrectSyntaxTest1()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);

                string origin =
                    "droplet; d1#";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

        [TestMethod]
        public void IncorrectSyntaxTest2()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);
                string origin =
                    "droplet d1;" +
                    "inputt(d1,10,10,3.2);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }


        //===========================================

        //===========================================

        [TestMethod]
        [ExpectedException(typeof(VariableNotReleasedException))]
        public void VariableNotReleasedTest1()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,3,3,3);\r\n" +
                "input(d1,3,3,3);";
            new Runner().DoCompile(origin);
        }

        //===========================================

        //===========================================

        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void ErrorDeclarationInputTest()
        {
            string origin =
                "droplet d1;" +
                "move(d1,3,3);";
            new Runner().DoCompile(origin);
        }


        //===========================================================================================//
        //                                                                                           //
        //                                    Test for REPEAT                                        //
        //                                                                                           //
        //===========================================================================================//
        [TestMethod]
        [ExpectedException(typeof(Exception))]
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
        [ExpectedException(typeof(BioException))]
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
        [ExpectedException(typeof(Exception))]
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