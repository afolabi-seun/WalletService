using System;
using Newtonsoft.Json;
using Step.Service.Model.Transaction;

namespace Step.Service.Model.Fee;

public class ListingResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("feesListing")]
    public List<FeesListing>? FeesListing { get; set; }
}

public class FeesListing
{
    [JsonProperty("FEES_ID")]
    public string? FeesId { get; set; }
    [JsonProperty("FEE_NAME")]
    public string? FeeName { get; set; }
    [JsonProperty("FEE_TYPE")]
    public string? FeeType { get; set; }
    [JsonProperty("CONVENIENCE_FEE")]
    public double? ConvenienceFee { get; set; }
    [JsonProperty("TRANSACTION_FEE")]
    public double? TransactionFee { get; set; }
    [JsonProperty("FLG_STATUS")]
    public string? FlgStatus { get; set; }
}

