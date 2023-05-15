using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects;

public class WalletRequest
{
    [Required]
    public string? ProfileId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? CountryCode { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? MobileNo { get; set; }

    public string? AccountNumber { get; set; }
    public string? AccountReference { get; set; }
    public string? AccountName { get; set; }
    public string? AccountType { get; set; }
    public string? BankName { get; set; }
    public string? BankCode { get; set; }
}

