using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletDeclaratorTest
    {
        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                   //
        //                                                                                           //
        //===========================================================================================//

        [TestMethod]
        public void CorrectInputTest()
        {
            string origin = "droplet d1;\r\ndroplet d2;\r\ndroplet d3;";
            string expect = "[\r\n  " +
                "{\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  " +
                "{\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  " +
                "{\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 3\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Robustness                                    //
        //                                                                                           //
        //===========================================================================================//
        [TestMethod]
        public void IncorrectSyntaxTest()
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
        public void IncorrectKeyword()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);

                string origin =
                    "droplettt d1;";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DropletDeclaredMoreThanOnceException))]
        public void DropletDeclaredMoreThanOnceTest()
        {
            string origin =
                "droplet d1;" +
                "droplet d1;";
            new Runner().DoCompile(origin);
        }

        [TestMethod]
        [ExpectedException(typeof(VariableNotReleasedException))]
        public void VariableNotReleasedTest1()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,3,3,3.2);\r\n" +
                "input(d1,3,3,3.2);";
            new Runner().DoCompile(origin);
        }
    }
}
