using System;
using Newtonsoft.Json;

namespace Step.Service.Model.PayGate
{
    public class OpenAccountResp
    {
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("data")]
        public Data? Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("provider_response_code")]
        public string? ProviderResponseCode { get; set; }
        [JsonProperty("provider")]
        public string? Provider { get; set; }
        [JsonProperty("errors")]
        public object? Errors { get; set; }
        [JsonProperty("error")]
        public object? Error { get; set; }
        [JsonProperty("provider_response")]
        public ProviderResponse? ProviderResponse { get; set; }
        [JsonProperty("client_info")]
        public ClientInfo? ClientInfo { get; set; }
    }

    public class ProviderResponse
    {
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("account_number")]
        public string? AccountNumber { get; set; }
        [JsonProperty("contract_code")]
        public object? contractCode { get; set; }
        [JsonProperty("account_reference")]
        public string? AccountReference { get; set; }
        [JsonProperty("account_name")]
        public string? AccountName { get; set; }
        [JsonProperty("currency_code")]
        public string? CurrencyCode { get; set; }
        [JsonProperty("customer_email")]
        public string? CustomerEmail { get; set; }
        [JsonProperty("bank_name")]
        public string? BankName { get; set; }
        [JsonProperty("bank_code")]
        public string? BankCode { get; set; }
        [JsonProperty("account_type")]
        public string? AccountType { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }
        [JsonProperty("meta")]
        public Meta? Meta { get; set; }
    }

    public class Meta
    {
    }

    public class ClientInfo
    {
        [JsonProperty("name")]
        public object? Name { get; set; }
        [JsonProperty("id")]
        public object? Id { get; set; }
        [JsonProperty("bank_cbn_code")]
        public object? BankCbnCode { get; set; }
        [JsonProperty("bank_name")]
        public object? BankName { get; set; }
        [JsonProperty("console_url")]
        public object? ConsoleUrl { get; set; }
        [JsonProperty("js_background_image")]
        public object? JsBackgroundImage { get; set; }
        [JsonProperty("css_url")]
        public object? CssUrl { get; set; }
        [JsonProperty("logo_url")]
        public object? logoUrl { get; set; }
        [JsonProperty("footer_text")]
        public object? FooterText { get; set; }
        [JsonProperty("show_options_icon")]
        public bool? ShowOptionsIcon { get; set; }
        [JsonProperty("paginate")]
        public bool? Paginate { get; set; }
        [JsonProperty("paginate_count")]
        public int? PaginateCount { get; set; }
        [JsonProperty("options")]
        public object? Options { get; set; }
        [JsonProperty("merchant")]
        public object? Merchant { get; set; }
        [JsonProperty("colors")]
        public object? Colors { get; set; }
        [JsonProperty("meta")]
        public object? Meta { get; set; }
    }
}

