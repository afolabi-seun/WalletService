using System;
using System.Text;
using Newtonsoft.Json;

using Step.Utils;
using Step.Utils.RandomNumber;
using Step.Utils.Cryptography;

using Step.BusinessServices;

using Step.ValueObjects;
using Step.ValueObjects.PayPlus.Disburse;
using Step.ValueObjects.PayPlus.WebHook;
using Step.ValueObjects.PayPlus.Query;

using Step.Service.Model.Transaction;
using Step.Service.Model.PayGate;
using System.Reflection;
using Step.Service.Model;
using Step.Service.Model.PayGate.Disburse;
using System.Security.Cryptography.Xml;

namespace Step.Service.Logic;

public interface ITransactionService
{
    ListingResp GetTransactions(string userId);
    DetailResp GetTransactionById(string refId);
    GenericResp CreateTransaction(TransactionRequest request);
    void CreateExternalTransaction(FidelityWebHookRequest request);
    bool ValidateSignature(string signature, string requestRef);
    CashOutResp CashOut(CashOutRequest request);
}

public class TransactionService : ITransactionService
{
    private readonly AppConfig _appConfig;
    private readonly ConfigService _cfgServ;

    public TransactionService(AppConfig appConfig, ConfigService cfgServ)
    {
        _appConfig = appConfig;
        _cfgServ = cfgServ;
    }

    public ListingResp GetTransactions(string userId)
    {
        var rsObj = new ListingResp();
        var response = _cfgServ.RetrieveTransaction(userId);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<ListingResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public DetailResp GetTransactionById(string refId)
    {
        var rsObj = new DetailResp();
        var response = _cfgServ.RetrieveTransactionByTxnId(refId);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<DetailResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public GenericResp CreateTransaction(TransactionRequest request)
    {
        var rsObj = new GenericResp();
        var response = _cfgServ.CreateTransaction(request);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<GenericResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public void CreateExternalTransaction(FidelityWebHookRequest request)
    {
        _cfgServ.CreateFdltyWbbHookTransaction(request);
    }

    public bool ValidateSignature(string signature, string requestRef)
    {
        if (requestRef == null)
            return false;

        var toHarsh = $"{requestRef};{_appConfig.PayGateSecret}";
        return Encryptor.VerifyMd5Hash(toHarsh, signature);
    }

    public CashOutResp CashOut(CashOutRequest request)
    {
        var rsObj = new CashOutResp();
        var cshOut = CreateDisburseRequest(request);
        var disburseResponse = DisburseTransaction(cshOut);
        if (disburseResponse != null)
        {
            var status = disburseResponse.Data?.ProviderResponseCode;
            if (status != null)
            {
                switch (status.ToLower())
                {
                    case "00":
                    case "07":
                        var cashout = new CashOutRequest
                        {
                            ProfileId = request.ProfileId,
                            CustomerRef = request.CustomerRef,
                            Amount = request.Amount,
                            TransactionFee = request.TransactionFee,
                            ConvenienceFee = request.ConvenienceFee,
                            FromCardNumber = request.FromCardNumber,
                            Status = disburseResponse.Status,
                            Message = disburseResponse.Message,
                            Narration = disburseResponse.Data?.ProviderResponse?.Narration,
                            PaymentId = disburseResponse.Data?.ProviderResponse?.PaymentId,
                            Reference = disburseResponse.Data?.ProviderResponse?.Reference,
                            ProviderResponseCode = disburseResponse.Data?.ProviderResponseCode,
                            OriginatorAccountName = disburseResponse.Data?.ProviderResponse?.OriginatorAccountName,
                            OriginatorAccountNumber = disburseResponse.Data?.ProviderResponse?.OriginatorAccountNumber,
                            BeneficiaryAccountNumber = disburseResponse.Data?.ProviderResponse?.BeneficiaryAccountNumber,
                            BeneficiaryAccountName = disburseResponse.Data?.ProviderResponse?.BeneficiaryAccountName,
                            DestinationInstitutionCode = disburseResponse.Data?.ProviderResponse?.DestinationInstitutionCode,
                            TransactionFinalAmount = disburseResponse.Data?.ProviderResponse?.TransactionFinalAmount
                        };
                        var response = _cfgServ.CashOut(cashout);
                        if (response != null)
                        {
                            var obj = JsonConvert.DeserializeObject<CashOutResp>(response);
                            if (obj != null)
                                rsObj = obj;
                        }
                        break;
                    case "400":
                    default:
                        rsObj.Status = disburseResponse.Status;
                        rsObj.Message = disburseResponse.Message;
                        rsObj.Error = disburseResponse.Data?.Error;
                        rsObj.Errors = disburseResponse.Data?.Errors;
                        break;
                }
            }
            else
            {
                rsObj.Status = disburseResponse.Status;
                rsObj.Message = disburseResponse.Message;
                rsObj.Error = disburseResponse.Data?.Error;
                rsObj.Errors = disburseResponse.Data?.Errors;
            }

        }
        else
        {
            if (disburseResponse != null)
            {
                rsObj.Status = disburseResponse.Status;
                rsObj.Message = disburseResponse.Message;
            }
            else
            {
                rsObj.Status = "Error";
                rsObj.Message = "An error occured. Cold not get a response from Fidelity API";
            }
        }
        return rsObj;
    }

    private static DisburseRequest CreateDisburseRequest(CashOutRequest request)
    {
        int Amount = ((int)(request.Amount - (request.TransactionFee + request.ConvenienceFee)));
        var cashOut = new DisburseRequest
        {
            request_ref = NumberGenerator.GenerateRandomNumbers(10, "reqRef-"),
            request_type = "disburse",
            Auth = new ValueObjects.PayPlus.Disburse.Auth
            {
                type = null,
                secure = null,
                auth_provider = "Fidelity",
                route_mode = null
            },
            Transaction = new ValueObjects.PayPlus.Disburse.Transaction
            {
                mock_mode = "Live",
                transaction_ref = NumberGenerator.GenerateRandomNumbers(10, "tnxRef-"),
                transaction_desc = request.TransactionDesc,
                transaction_ref_parent = null,
                amount = (Amount * 100),
                Customer = new Customer
                {
                    customer_ref = request.CustomerRef,
                    firstname = request.FirstName,
                    surname = request.Surname,
                    email = request.Email,
                    mobile_no = request.MobileNo,
                },
                Meta = new ValueObjects.PayPlus.Disburse.Meta
                {
                    a_key = "a_meta_value_1",
                    b_key = "a_meta_value_2"
                },
                Details = new ValueObjects.PayPlus.Disburse.Details
                {
                    destination_account = request.DestinationAccount,
                    destination_bank_code = request.DestinationBankCode
                }
            }
        };
        return cashOut;
    }
    public DisburseResp DisburseTransaction(DisburseRequest model)
    {
        var resp = string.Empty;
        var fldResp = new DisburseResp();

        var md5 = $"{model.request_ref};{_appConfig.PayGateSecret}";
        var _signature = Encryptor.GetMd5Hash(md5);
        var apiKey = $"Bearer {_appConfig.PayGateApi}";

        var data = JsonConvert.SerializeObject(model);
        var request = new StringContent(data, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"{apiKey}");
        client.DefaultRequestHeaders.Add("Signature", $"{_signature}");

        var url = $"{_appConfig.PayGateUri}{_appConfig.PayGateTransctionsUri}";
        var response = client.PostAsync(url, request).Result;

        if (response != null)
        {
            var json = response.Content.ReadAsStringAsync().Result;
            if (json != "[]")
            {
                fldResp = JsonConvert.DeserializeObject<DisburseResp>(json)!;
            }
        }
        else
        {
            fldResp.Status = "Error";
            fldResp.Message = "An error occured. Cold not get a response from Fidelity API";
        }

        return fldResp;
    }
    private static ReQueryTransactionRequest CreateQueryRequest(ReQueryTransactionRequest request)
    {
        var queryTransaction = new ReQueryTransactionRequest
        {
            request_ref = request.request_ref,
            request_type = "disburse",
            Auth = new ValueObjects.PayPlus.Query.Auth
            {
                secure = null,
                auth_provider = "FidelityVirtual"
            },
            Transaction = new ValueObjects.PayPlus.Query.Transaction
            {
                transaction_ef = request.request_ref
            }
        };
        return queryTransaction;
    }
    public QueryTransactionResp QueryTransaction(ReQueryTransactionRequest model)
    {
        var resp = string.Empty;
        var fldResp = new QueryTransactionResp();

        var md5 = $"{model.request_ref};{_appConfig.PayGateSecret}";
        var _signature = Encryptor.GetMd5Hash(md5);
        var apiKey = $"Bearer {_appConfig.PayGateApi}";

        var data = JsonConvert.SerializeObject(model);
        var request = new StringContent(data, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"{apiKey}");
        client.DefaultRequestHeaders.Add("Signature", $"{_signature}");

        var url = $"{_appConfig.PayGateUri}{_appConfig.PayGateQueryUri}";
        var response = client.PostAsync(url, request).Result;

        if (response != null)
        {
            var json = response.Content.ReadAsStringAsync().Result;
            if (json != "[]")
            {
                fldResp = JsonConvert.DeserializeObject<QueryTransactionResp>(json)!;
            }
        }
        else
        {
            fldResp.Status = "Error";
            fldResp.Message = "An error occured. Cold not get a response from Fidelity API";
        }

        return fldResp;
    }
}


