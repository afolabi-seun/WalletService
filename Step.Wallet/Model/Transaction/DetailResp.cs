using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Transaction;

public class DetailResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("transactionDetails")]
    public List<TransactionDetail>? transactionDetails { get; set; }
}

public class TransactionDetail
{
    [JsonProperty("WALLET_ID")]
    public string? WalletId { get; set; }
    [JsonProperty("CARD_NUM")]
    public string? CardNumber { get; set; }
    [JsonProperty("REFERENCE_ID")]
    public string? ReferenceId { get; set; }
    [JsonProperty("CR")]
    public string? Cr { get; set; }
    [JsonProperty("DR")]
    public string? Dr { get; set; }
    [JsonProperty("BALANCE")]
    public string? Balance { get; set; }
    [JsonProperty("LAST_UPDATE_DATE")]
    public string? LastUpdateDate { get; set; }
}

