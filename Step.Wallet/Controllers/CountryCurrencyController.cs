using System;
using Step.ValueObjects;
using Step.Service.Logic;
using Microsoft.AspNetCore.Mvc;
using Step.Service.Authorization;

namespace Step.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CountryCurrencyController : ControllerBase
{
    private readonly ICountryCurrencyService _service;

    public CountryCurrencyController(ICountryCurrencyService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll(string userId)
    {
        var ccys = _service.GetCountryCurrency(userId);
        return Ok(ccys);
    }

    [HttpPost("createCountryCurrency")]
    public IActionResult Register(CountryCurrencyRequest model)
    {
        var ccy = _service.CreateCountryCurrency(model);
        return Ok(ccy); ;
    }
}

