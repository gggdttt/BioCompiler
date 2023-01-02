// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletInputTest
    {

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                      //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// Basic use of input
        /// </summary>
        [TestMethod]
        public void FunctionTest()
        {
            string origin = 
                "droplet d1;\r\n\r\n" +
                "input(d1,10,10,0.025);";
            string expect = 
                "[\r\n  " +
                "{\r\n    " +
                "\"$type\": " +
                "\"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    " +
                "\"name\": \"d1\",\r\n    " +
                "\"line\": 1\r\n  " +
                "},\r\n  " +
                "{\r\n    " +
                "\"$type\": " +
                "\"Executor.Model.Operation.DropletInputer, Executor\",\r\n    " +
                "\"line\": 3,\r\n    " +
                "\"name\": \"d1\",\r\n    " +
                "\"xValue\": 10,\r\n    " +
                "\"yValue\": 10,\r\n    " +
                "\"size\": 0.025\r\n  " +
                "}\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// No exception throw
        /// </summary>
        [TestMethod]
        public void DropletInputThenOutputTest()
        {
            string origin =
                "droplet d1; " +
                "input(d1,10,10,0.025);" +
                "output(d1,0,0);" +
                "input(d1,10,10,0.025);";
            new Runner().DoCompile(origin);
        }

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Robustness                                    //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// The expected value is double, but input is int.
        /// TIP: Antlr will throw a nullReference Exception because of the its inner exception handler.
        /// It is a bit different from the KeyWord error.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectSyntaxException))]
        public void IncorrectSyntaxTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);

                string origin =
                    "droplet d1; " +
                    "input(d1,10,10,1);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectSyntaxException))]
        public void IncorrectSyntaxTest2()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);

                string origin =
                    "droplet d1; " +
                    "input(d1,a,a,a);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectSyntaxException))]
        public void IncorrectSyntaxTest3()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);

                string origin =
                    "droplet d1; " +
                    "input(10,a,a,a);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

        /// <summary>
        /// Keyword error, will not throw an exception
        /// </summary>
        [TestMethod]
        public void IncorrectKeyWord()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);

                string origin =
                    "droplet d1; " +
                    "inputttt(d1,10,10,0.025);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(VariableNotReleasedException))]
        public void DropletInputMoreThanOnceTest()
        {
            string origin =
                "droplet d1; " +
                "input(d1,10,10,0.025);"+
                "input(d1,10,10,0.025);";
            new Runner().DoCompile(origin);
        }

    }
}
