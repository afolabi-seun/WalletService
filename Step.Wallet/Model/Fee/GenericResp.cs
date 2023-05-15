using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Fee;

public class GenericResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("FeeId")]
    public string? FeeId { get; set; }
}

