using System;
using Newtonsoft.Json;

namespace Step.Service.Model.CountryCurrency;

public class GenericResp
{
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("CountryId")]
    public string? CountryCurrencyId { get; set; }
}

