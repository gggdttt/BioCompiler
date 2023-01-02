// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using Executor.Model.Operation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Executor.Model.OperationHelper
{
    /// <summary>
    /// This JsonConverter convert the JSON format operations to C# object
    /// </summary>
    public class CompilerOperation2JSONConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(CompilerOperation);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            return GetOperation(jo);
        }

        private object GetOperation(JObject jo)
        {
            string type = jo["$type"]!.Value<string>()!;
            jo.Remove("$type");
            if ("Executor.Model.Operation.RepeatOperation, Executor".Equals(type))
            {
                string repeatContent = jo["repeatOperations"]!.ToString();
                // Is a repeat operation, do load Recursively
                List<CompilerOperation> operations = new List<CompilerOperation>();
                RepeatOperation repeatOperation = new RepeatOperation(jo["line"]!.Value<int>(), jo["repeatTimes"]!.Value<int>());

                var currencyArray = JArray.Parse(repeatContent).Children<JObject>();
                foreach (var temp in currencyArray)
                {
                    repeatOperation.repeatOperations.Add((CompilerOperation)GetOperation(JObject.Parse(temp.ToString())));
                }
                return repeatOperation;
            }
            else
            {
                // remove the property type before Deserialize
                // because there is not such member in compiler operation classes
                return jo.ToObject(Type.GetType(type)!);
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

    }
}
