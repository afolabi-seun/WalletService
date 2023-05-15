using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Wallet;

public class CreateResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("errors")]
    public object? Errors { get; set; }
    [JsonProperty("error")]
    public object? Error { get; set; }
    [JsonProperty("walletDetails")]
    public WalletDetails? WalletDetails { get; set; }
}

public class WalletDetails
{
    [JsonProperty("WALLET_ID")]
    public string? WalletId { get; set; }
    [JsonProperty("PROFILE_ID")]
    public string? ProfileId { get; set; }
    [JsonProperty("WALLET_TYPE")]
    public string? WalletType { get; set; }
    [JsonProperty("WALLET_NUMBER")]
    public string? WalletNumber { get; set; }
    [JsonProperty("BANK_NAME")]
    public string? BankName { get; set; }
    [JsonProperty("BANK_CODE")]
    public string? BankCode { get; set; }
    [JsonProperty("ACCOUNT_NUMBER")]
    public string? AccountNumber { get; set; }
    [JsonProperty("BALANCE")]
    public double? Balance { get; set; }
    [JsonProperty("FLG_STATUS")]
    public string? FlgStatus { get; set; }
}

