// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using Executor.Model.Operation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Executor.Model.OperationHelper
{
    public class CompilerOperation2JSONConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(CompilerOperation);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            string type = jo["$type"]!.Value<string>()!;
            // remove the property type before Deserialize
            // because there is not such member in compiler operation classes
            jo.Remove("$type");
            return jo.ToObject(Type.GetType(type)!);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
