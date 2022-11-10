using Newtonsoft.Json;
using System.Text;
using BioCompiler;
using BioCompiler.Compiler;
using System.Collections.Immutable;


namespace TestCompiler
{
    [TestClass]
    public class DropletDeclarationTest
    {
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

        [TestMethod]
        public void TestMethod1()
        {
            string origin = "droplet d1;\r\ndroplet d2;\r\ndroplet d3;";
            string expect = "[\r\n  " +
                "{\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  " +
                "{\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  " +
                "{\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 3\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }
    }
}