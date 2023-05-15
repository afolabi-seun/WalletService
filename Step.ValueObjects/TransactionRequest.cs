using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects;

public class TransactionRequest
{
    [Required]
    public string? TransactionRef { get; set; }
    [Required]
    public string? FromCardNumber { get; set; }
    [Required]
    public string? ToCardNumber { get; set; }
    [Required]
    public int TotalAmount { get; set; }
    [Required]
    public int TransactionFee { get; set; }
    [Required]
    public int ConvenienceFee { get; set; }
    [Required]
    public string? Description { get; set; }
    public string? TransactionType { get; set; }
}



