using System;
namespace Step.Service.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}

