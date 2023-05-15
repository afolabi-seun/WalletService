using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects;

public class WalletUpdateRequest
{
    [Required]
    public string? WalletId { get; set; }
    [Required]
    public char? Status { get; set; }
}

