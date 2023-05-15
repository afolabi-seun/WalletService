using System;
using System.Text;
using Newtonsoft.Json;

using Step.Utils;

using Step.ValueObjects;
using Step.BusinessServices;

using Step.Service.Model.CountryCurrency;

namespace Step.Service.Logic;

public interface ICountryCurrencyService
{
    ListingResp GetCountryCurrency(string userId);
    GenericResp CreateCountryCurrency(CountryCurrencyRequest request);
}

public class CountryCurrencyService : ICountryCurrencyService
{
    private readonly AppConfig _appConfig;
    private readonly ConfigService _cfgServ;

    public CountryCurrencyService(AppConfig appConfig, ConfigService cfgServ)
    {
        _appConfig = appConfig;
        _cfgServ = cfgServ;
    }

    public ListingResp GetCountryCurrency(string userId)
    {
        var rsObj = new ListingResp();
        var response = _cfgServ.RetrieveCountryCurrency(userId);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<ListingResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public GenericResp CreateCountryCurrency(CountryCurrencyRequest request)
    {
        var rsObj = new GenericResp();
        var response = _cfgServ.CreateCountryCurrency(request);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<GenericResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

}