using System;
using System.Text;
using Newtonsoft.Json;

using Step.Utils;
using Step.Utils.RandomNumber;
using Step.Utils.Cryptography;

using Step.BusinessServices;

using Step.ValueObjects;
using Step.ValueObjects.PayPlus.Accounts;

using Step.Service.Model.PayGate;
using Step.Service.Model.Wallet;


namespace Step.Service.Logic;

public interface IWalletService
{
    ListingResp GetCards(string userId);
    DetailResp GetCardById(string walletId);
    CreateResp CreateVirtualCard(WalletRequest request);
    GenericResp UpdateVirtualCard(WalletUpdateRequest request);
}

public class WalletService : IWalletService
{
    private readonly AppConfig _appConfig;
    private readonly ConfigService _cfgServ;

    public WalletService(AppConfig appConfig, ConfigService cfgServ)
    {
        _appConfig = appConfig;
        _cfgServ = cfgServ;
    }

    public ListingResp GetCards(string userId)
    {
        var rsObj = new ListingResp();
        var response = _cfgServ.RetrieveWallet(userId);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<ListingResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public DetailResp GetCardById(string walletId)
    {
        var rsObj = new DetailResp();
        var response = _cfgServ.RetrieveWalletById(walletId);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<DetailResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public CreateResp CreateVirtualCard(WalletRequest request)
    {
        var rsObj = new CreateResp();

        var openAccount = CreateAccountRequest(request);
        var bankResponse = OpenAccount(openAccount);
        if (bankResponse != null)
        {
            var status = bankResponse.Data?.ProviderResponseCode;
            if (status != null)
            {
                switch (status.ToLower())
                {
                    case "00":
                        var walletRequest = new WalletRequest
                        {
                            AccountNumber = bankResponse.Data?.ProviderResponse?.AccountNumber,
                            AccountReference = bankResponse.Data?.ProviderResponse?.AccountReference,
                            AccountName = bankResponse.Data?.ProviderResponse?.AccountName,
                            AccountType = bankResponse.Data?.ProviderResponse?.AccountType,
                            BankName = bankResponse.Data?.ProviderResponse?.BankName,
                            BankCode = bankResponse.Data?.ProviderResponse?.BankCode,
                            ProfileId = request.ProfileId,
                            FirstName = request.FirstName,
                            Surname = request.Surname,
                            CountryCode = request.CountryCode,
                            Email = request.Email,
                            MobileNo = request.MobileNo
                        };
                        var response = _cfgServ.CreateWallet(walletRequest);
                        if (response != null)
                        {
                            var obj = JsonConvert.DeserializeObject<CreateResp>(response);
                            if (obj != null)
                                rsObj = obj;
                        }
                        break;
                    case "400":
                    default:
                        rsObj.Status = bankResponse.Status;
                        rsObj.Message = bankResponse.Message;
                        rsObj.Error = bankResponse.Data?.Error;
                        rsObj.Errors = bankResponse.Data?.Errors;
                        break;
                }
            }
            else
            {
                rsObj.Status = bankResponse.Status;
                rsObj.Message = bankResponse.Message;
                rsObj.Error = bankResponse.Data?.Error;
                rsObj.Errors = bankResponse.Data?.Errors;
            }
        }
        else
        {
            if (bankResponse != null)
            {
                rsObj.Status = bankResponse.Status;
                rsObj.Message = bankResponse.Message;
            }
            else
            {
                rsObj.Status = "Error";
                rsObj.Message = "An error occured. Cold not get a response from Fidelity API";
            }
        }

        return rsObj;
    }

    public GenericResp UpdateVirtualCard(WalletUpdateRequest request)
    {
        var rsObj = new GenericResp();

        var response = _cfgServ.UpdateWalletById(request);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<GenericResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    private OpenAccountRequest CreateAccountRequest(WalletRequest request)
    {
        var openAccount = new OpenAccountRequest
        {
            request_ref = NumberGenerator.GenerateRandomNumbers(10, "reqRef-"),
            request_type = "open_account",
            Auth = new Auth
            {
                type = null,
                secure = null,
                auth_provider = "FidelityVirtual",
                route_mode = null
            },
            Transaction = new Transaction
            {
                mock_mode = "Live",
                transaction_ref = NumberGenerator.GenerateRandomNumbers(10, "tnxRef-"),
                transaction_desc = "A step account creation",
                transaction_ref_parent = null,
                amount = 0,
                Customer = new Customer
                {
                    customer_ref = NumberGenerator.GenerateRandomNumbers(9, "custRef-"), //request.CustomerRef,
                    firstname = request.FirstName,
                    surname = request.Surname,
                    email = request.Email,
                    mobile_no = request.MobileNo,
                },
                Meta = new ValueObjects.PayPlus.Accounts.Meta
                {
                    a_key = "a_meta_value_1",
                    b_key = "a_meta_value_2"
                },
                Details = new ValueObjects.PayPlus.Accounts.Details
                {
                    name_on_account = $"{request.FirstName} {request.Surname}",
                    middlename = request.Surname,
                    dob = "",
                    gender = "",
                    title = "",
                    address_line_1 = "",
                    address_line_2 = "",
                    city = "",
                    state = "",
                    country = "Nigeria"
                }
            }
        };
        return openAccount;
    }
    private OpenAccountResp OpenAccount(OpenAccountRequest model)
    {
        var resp = string.Empty;
        var fldResp = new OpenAccountResp();

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
                fldResp = JsonConvert.DeserializeObject<OpenAccountResp>(json)!;
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

