using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Transaction;

public class CashOutResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("errors")]
    public object? Errors { get; set; }
    [JsonProperty("error")]
    public object? Error { get; set; }
}

