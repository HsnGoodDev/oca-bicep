using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace oca.model;

public class ResponseMessage {
    [JsonProperty("response_message")]
    public string Message {get; set;}
}