using System;
using Step.ValueObjects;
using Step.Service.Logic;
using Microsoft.AspNetCore.Mvc;
using Step.Service.Authorization;

namespace Step.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _service;

    public WalletController(IWalletService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll(string userId)
    {
        var cards = _service.GetCards(userId);
        return Ok(cards);
    }

    [HttpGet("{walletId}")]
    public IActionResult GetById(string walletId)
    {
        var card = _service.GetCardById(walletId);
        return Ok(card);
    }

    [HttpPost("requestCard")]
    public IActionResult Register(WalletRequest model)
    {
        var response = _service.CreateVirtualCard(model);
        return Ok(response);
    }

    [HttpPut]
    public IActionResult Update(WalletUpdateRequest model)
    {
        var response = _service.UpdateVirtualCard(model);
        return Ok(response);
    }
}

