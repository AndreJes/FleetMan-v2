using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace FleetManApiController.Configuration
{
    public static class JsonConfig
    {
        public static JsonSerializerSettings DefaultSettings => new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static JsonMediaTypeFormatter DefaultJsonMediaType => new JsonMediaTypeFormatter()
        {
            SerializerSettings = DefaultSettings
        };
    }
}
