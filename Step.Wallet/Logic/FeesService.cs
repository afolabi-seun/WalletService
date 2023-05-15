using System;
using System.Text;
using Newtonsoft.Json;

using Step.Utils;

using Step.ValueObjects;
using Step.BusinessServices;

using Step.Service.Model.Fee;

namespace Step.Service.Logic;

public interface IFeeService
{
    ListingResp GetFees(string userId);
    GenericResp CreateFee(FeeRequest request);
}

public class FeeService : IFeeService
{
    private readonly AppConfig _appConfig;
    private readonly ConfigService _cfgServ;

    public FeeService(AppConfig appConfig, ConfigService cfgServ)
    {
        _appConfig = appConfig;
        _cfgServ = cfgServ;
    }

    public ListingResp GetFees(string userId)
    {
        var rsObj = new ListingResp();
        var response = _cfgServ.RetrieveFees(userId);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<ListingResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

    public GenericResp CreateFee(FeeRequest request)
    {
        var rsObj = new GenericResp();
        var response = _cfgServ.CreateFees(request);
        if (response != null)
        {
            var obj = JsonConvert.DeserializeObject<GenericResp>(response);
            if (obj != null)
                rsObj = obj;
        }

        return rsObj;
    }

}