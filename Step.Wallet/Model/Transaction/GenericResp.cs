using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Transaction;

public class GenericResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("TxnRef")]
    public string? TxnRef { get; set; }
    [JsonProperty("TxnId")]
    public string? TxnId { get; set; }
}

