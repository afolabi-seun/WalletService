using System;
using Step.ValueObjects;
using Step.Service.Logic;
using Microsoft.AspNetCore.Mvc;
using Step.Service.Authorization;

namespace Step.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FeeController : ControllerBase
{
    private readonly IFeeService _service;

    public FeeController(IFeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll(string userId)
    {
        var fees = _service.GetFees(userId);
        return Ok(fees);
    }

    [HttpPost("createFee")]
    public IActionResult Register(FeeRequest model)
    {
        var fee = _service.CreateFee(model);
        return Ok(fee);
    }
}

