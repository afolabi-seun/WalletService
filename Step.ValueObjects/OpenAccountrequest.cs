using System;
using System.ComponentModel.DataAnnotations;

namespace Step.ValueObjects.PayPlus.Accounts;

public class OpenAccountRequest
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
    public int? amount { get; set; }
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
    public string? name_on_account { get; set; }
    public string? middlename { get; set; }
    public string? dob { get; set; }
    public string? gender { get; set; }
    public string? title { get; set; }
    public string? address_line_1 { get; set; }
    public string? address_line_2 { get; set; }
    public string? city { get; set; }
    public string? state { get; set; }
    public string? country { get; set; }
}

