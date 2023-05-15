using System;
using Newtonsoft.Json;
using Step.Service.Model.Wallet;

namespace Step.Service.Model.CountryCurrency;

public class ListingResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("countryCurrencyListing")]
    public List<CountryCurrencyListing>? CountryCurrencyListing { get; set; }
}

public class CountryCurrencyListing
{
    [JsonProperty("COUNTRY_ID")]
    public string? CountryId { get; set; }
    [JsonProperty("COUNTRY_NAME")]
    public string? CountryName { get; set; }
    [JsonProperty("COUNTRY_CODE")]
    public string? CountryCode { get; set; }
    [JsonProperty("CURRENCY_CODE")]
    public string? CurrencyCode { get; set; }
    [JsonProperty("BALANCE")]
    public double? Balance { get; set; }
    [JsonProperty("FLG_STATUS")]
    public string? FlgStatus { get; set; }
}


