using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Step.ValueObjects;

public class CashOutRequest
{
    [Required]
    public string? ProfileId { get; set; }
    [Required]
    public string? CustomerRef { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? MobileNo { get; set; }
    [Required]
    public string? FromCardNumber { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public double TransactionFee { get; set; }
    [Required]
    public double ConvenienceFee { get; set; }
    [Required]
    public string? TransactionDesc { get; set; }
    [Required]
    public string? DestinationAccount { get; set; }
    [Required]
    public string? DestinationBankCode { get; set; }


    public string? Status { get; set; }
    public string? Message { get; set; }
    public string? PaymentId { get; set; }
    public string? Reference { get; set; }
    public string? ProviderResponseCode { get; set; }
    public int? TransactionFinalAmount { get; set; }
    public string? Narration { get; set; }
    public string? OriginatorAccountName { get; set; }
    public string? OriginatorAccountNumber { get; set; }
    public string? BeneficiaryAccountNumber { get; set; }
    public string? BeneficiaryAccountName { get; set; }
    public string? DestinationInstitutionCode { get; set; }
}

