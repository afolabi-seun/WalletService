using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects.PayPlus.WebHook;

public class FidelityWebHookRequest
{
    public string? Request_Ref { get; set; }
    public string? Request_Type { get; set; }
    public string? Requester { get; set; }
    public string? Mock_Mode { get; set; }
    public Details? Details { get; set; }
    public AppInfo? App_Info { get; set; }
}

public class AppInfo
{
    public string? App_Code { get; set; }
}

public class Details
{
    public int Amount { get; set; }
    public string? Status { get; set; }
    public string? Provider { get; set; }
    public string? Customer_Ref { get; set; }
    public string? Customer_Email { get; set; }
    public string? Transaction_Ref { get; set; }
    public string? Customer_Surname { get; set; }
    public string? Transaction_Desc { get; set; }
    public string? Transaction_Type { get; set; }
    public string? Customer_Firstname { get; set; }
    public string? Customer_Mobile_No { get; set; }
    public Data? Data { get; set; }
}

public class Data
{
    public string? Tag { get; set; }
    public string? Amount { get; set; }
    public string? BankCode { get; set; }
    public string? BankName { get; set; }
    public DateTime TranDate { get; set; }
    public string? Bank_Code { get; set; }
    public string? CrAccount { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? Narration { get; set; }
    public string? SessionId { get; set; }
    public string? ChannelCode { get; set; }
    public string? ChargeAmount { get; set; }
    public string? CrAccountName { get; set; }
    public string? OriginatorBvn { get; set; }
    public string? Account_Number { get; set; }
    public string? BeneficiaryBvn { get; set; }
    public string? NameEnquiryRef { get; set; }
    public string? OriginatorName { get; set; }
    public string? PaymentReference { get; set; }
    public string? OriginatorCbnCode { get; set; }
    public string? OriginatorKycLevel { get; set; }
    public string? BeneficiaryKycLevel { get; set; }
    public string? TransactionLocation { get; set; }
    public string? CollectionAccountNumber { get; set; }
    public string? OriginatorAccountNumber { get; set; }
    public string? DestinationInstitutionBankCode { get; set; }
}

