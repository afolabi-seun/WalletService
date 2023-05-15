using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Wallet;

public class DetailResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    public WalletDetail? WalletDetail { get; set; }
}

public class WalletDetail
{
    [JsonProperty("WALLET_ID")]
    public string? WalletId { get; set; }
    [JsonProperty("PROFILE_ID")]
    public string? ProfileId { get; set; }
    [JsonProperty("WALLET_TYPE")]
    public string? WalletType { get; set; }
    [JsonProperty("WALLET_NUMBER")]
    public string? WalletNumber { get; set; }
    [JsonProperty("BALANCE")]
    public double? Balance { get; set; }
    [JsonProperty("FLG_STATUS")]
    public string? FlgStatus { get; set; }
    [JsonProperty("LAST_UPDATE_DATE")]
    public string? LastUpdateDate { get; set; }
}

