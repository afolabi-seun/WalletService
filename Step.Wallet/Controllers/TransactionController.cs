using System;
using System.Net;
using Step.ValueObjects;
using Step.Service.Logic;
using Microsoft.AspNetCore.Mvc;
using Step.Service.Authorization;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.PortableExecutable;
using Step.ValueObjects.PayPlus.WebHook;

namespace Step.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll(string userId)
    {
        var transactions = _service.GetTransactions(userId);
        return Ok(transactions);
    }

    [HttpGet("{refId}")]
    public IActionResult GetById(string refId)
    {
        var transaction = _service.GetTransactionById(refId);
        return Ok(transaction);
    }

    [HttpPost("sendFunds")]
    public IActionResult Register(TransactionRequest model)
    {
        var response = _service.CreateTransaction(model);
        return Ok(response);
    }

    [HttpPost("cashOutFunds")]
    public IActionResult Transfer(CashOutRequest model)
    {
        var response = _service.CashOut(model);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("FidelityWebhook")]
    public IActionResult FidelityWebHookTransaction(FidelityWebHookRequest model)
    {
        //was the header provided?
        var signatureHeaders = Request.Headers["Signature"].FirstOrDefault();
        if (signatureHeaders == null) return BadRequest(); //401

        //does the signature match  exist?
        if (_service.ValidateSignature(signatureHeaders, model.Request_Ref!) == false) return StatusCode(403); //403

        _service.CreateExternalTransaction(model);
        return Ok(new { status = "Successful", message = "Transaction received successfully" }); //200
    }
}

