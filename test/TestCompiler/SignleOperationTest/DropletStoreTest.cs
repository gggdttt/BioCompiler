// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletStoreTest
    {

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                      //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// Basic use of Store
        /// </summary>
        [TestMethod]
        public void StoreFunctionalityTest1()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,10,10,0.1);\r\n" +
                "store(d1,5,5,2.0);";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 2,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 10,\r\n    \"yValue\": 10,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletStorer, Executor\",\r\n    \"line\": 3,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 5,\r\n    \"yValue\": 5,\r\n    \"latency\": 2.0\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }


        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Robustness                                    //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// D1 is declared but not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void DropletDeclaredButNotAssignedValue()
        {
            string origin =
                "droplet d1;\r\n" +
                "store(d1,5,5,2.0);";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D1 is not declared and not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclared()
        {
            string origin = "store(d1,5,5,2.0);";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// Key word error
        /// </summary>
        [TestMethod]
        public void KeyWordError()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);
                string origin =
                    "droplet d1;\r\n" +
                    "input(d1,10,10,0.1);\r\n" +
                    "storeeeee(d1,5,5,2.0);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

    }
}
