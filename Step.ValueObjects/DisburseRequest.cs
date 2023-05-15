using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects.PayPlus.Disburse;

public class DisburseRequest
{
    public string? request_ref { get; set; }
    public string? request_type { get; set; }
    public Auth? Auth { get; set; }
    public Transaction? Transaction { get; set; }
}

public class Auth
{
    public object? type { get; set; }
    public object? secure { get; set; }
    public string? auth_provider { get; set; }
    public object? route_mode { get; set; }
}

public class Transaction
{
    public string? mock_mode { get; set; }
    public string? transaction_ref { get; set; }
    public string? transaction_desc { get; set; }
    public object? transaction_ref_parent { get; set; }
    public int amount { get; set; }
    public Customer? Customer { get; set; }
    public Meta? Meta { get; set; }
    public Details? Details { get; set; }
}

public class Customer
{
    public string? customer_ref { get; set; }
    public string? firstname { get; set; }
    public string? surname { get; set; }
    public string? email { get; set; }
    public string? mobile_no { get; set; }
}

public class Meta
{
    public string? a_key { get; set; }
    public string? b_key { get; set; }
}

public class Details
{
    public string? destination_account { get; set; }
    public string? destination_bank_code { get; set; }
}


