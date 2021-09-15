using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBKM.Presentation.models
{
    public class ServiceResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int status { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string message { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Object data { get; set; }
    }
}