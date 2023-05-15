using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Wallet;

public class GenericResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("WalletId")]
    public string? WalletId { get; set; }
}

