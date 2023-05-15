using System;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;

namespace Step.Service.Model.Auth;

public class AuthenticateResponse
{
    [JsonProperty("ProfileId")]
    public string? ProfileId { get; set; }
    [JsonProperty("Status")]
    public string? Status { get; set; }
    [JsonProperty("Errors")]
    public List<object>? Errors { get; set; }
    [JsonProperty("StatusText")]
    public string? StatusText { get; set; }
    [JsonProperty("Data")]
    public string? Data { get; set; }
    [JsonProperty("Type")]
    public string? Type { get; set; }
    [JsonProperty("Title")]
    public string? Title { get; set; }
    [JsonProperty("TraceId")]
    public string? TraceId { get; set; }
}

