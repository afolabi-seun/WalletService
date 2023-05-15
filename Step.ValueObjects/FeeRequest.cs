using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects;

public class FeeRequest
{
    [Required]
    public string? ProfileId { get; set; }
    [Required]
    public string? FeeName { get; set; }
    [Required]
    public string? FeeType { get; set; }
    [Required]
    public double? ConvenienceFee { get; set; }
    [Required]
    public double? TransactionFee { get; set; }
}

