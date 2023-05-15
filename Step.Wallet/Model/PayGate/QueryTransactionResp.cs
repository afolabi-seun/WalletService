using System;
using Newtonsoft.Json;

namespace Step.Service.Model.PayGate
{
    public class QueryTransactionResp
    {
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("data")]
        public QData? Data { get; set; }
    }

    public class QData
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
        public QProviderResponse? ProviderResponse { get; set; }
        [JsonProperty("client_info")]
        public QClientInfo? ClientInfo { get; set; }
    }

    public class QProviderResponse
    {
        [JsonProperty("destination_institution_code")]
        public string? DestinationInstitution_code { get; set; }
        [JsonProperty("beneficiary_account_name")]
        public string? BeneficiaryAccountName { get; set; }
        [JsonProperty("beneficiary_account_number")]
        public string? BeneficiaryAccountNumber { get; set; }
        [JsonProperty("beneficiary_kyc_level")]
        public string? BeneficiaryKycLevel { get; set; }
        [JsonProperty("originator_account_name")]
        public string? OriginatorAccountName { get; set; }
        [JsonProperty("originator_account_number")]
        public string? OriginatorAccountNumber { get; set; }
        [JsonProperty("originator_kyc_level")]
        public string? OriginatorKycLevel { get; set; }
        [JsonProperty("narration")]
        public string? Narration { get; set; }
        [JsonProperty("transaction_final_amount")]
        public int? TransactionFinalAmount { get; set; }
        [JsonProperty("reference")]
        public string? ReferenceId { get; set; }
        [JsonProperty("payment_id")]
        public string? PaymentId { get; set; }
    }

    public class QClientInfo
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

