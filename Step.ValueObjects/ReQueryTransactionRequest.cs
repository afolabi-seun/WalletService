using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects.PayPlus.Query;

public class ReQueryTransactionRequest
{
    public string? request_ref { get; set; }
    public string? request_type { get; set; }
    public Auth? Auth { get; set; }
    public Transaction? Transaction { get; set; }
}

public class Auth
{
    public object? secure { get; set; }
    public string? auth_provider { get; set; }
}

public class Transaction
{
    public string? transaction_ef { get; set; }
}

