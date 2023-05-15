using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects;

public class CountryCurrencyRequest
{
    [Required]
    public string? ProfileId { get; set; }
    [Required]
    public string? CountryName { get; set; }
    [Required]
    public string? CountryCode { get; set; }
    [Required]
    public string? CurrencyCode { get; set; }
}

