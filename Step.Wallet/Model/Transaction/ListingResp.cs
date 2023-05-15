using System;
using Newtonsoft.Json;

namespace Step.Service.Model.Transaction;

public class ListingResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("transactionListing")]
    public List<TransactionListing>? TransactionListing { get; set; }
}

public class TransactionListing
{
    [JsonProperty("TXN_DATE")]
    public string? TransactionDate { get; set; }
    [JsonProperty("TXN_ID")]
    public string? TransactionId { get; set; }
    [JsonProperty("TXN_TYPE")]
    public string? TransactionType { get; set; }
    [JsonProperty("REFERENCE_ID")]
    public string? ReferenceId { get; set; }
    [JsonProperty("FROM_WALLET_NUMBER")]
    public string? FromWallet { get; set; }
    [JsonProperty("TO_WALLET_NUMBER")]
    public string? ToWallet { get; set; }
    [JsonProperty("AMOUNT")]
    public string? Amount { get; set; }
    [JsonProperty("DESCRIPTION")]
    public string? Description { get; set; }
    [JsonProperty("STATUS_CODE")]
    public string? StatusCode { get; set; }
}

