using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace Step.Utils;

public class AppConfig
{
    private IConfiguration _configuration;

    private static string? _strCon;
    private static string? _con;

    public AppConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #region ConnectionString

    public string? GeConnectionString()
    {
        _con = _configuration.GetConnectionString("StepConnectionString");
        if(_con != null)
        {
            _strCon = _con;
        }
        else
        {
            _con = _configuration.GetConnectionString("StepConnectionString");
            _strCon = _con;
        }
            return _strCon;
    }

    #endregion

    private string GetAppValue(string section, string subSection)
    {
        return _configuration[$"AppSettings:{section}:{subSection}"]!;
    }

    private string GetValue(string key, string  subKey)
    {
        return GetAppValue(key, subKey);
    }

    private string GetAppValue(string mainSection, string subSection, string section)
    {
        return _configuration[$"AppSettings:{mainSection}:{subSection}:{section}"]!;
    }

    private string GetValue(string mainKey, string subKey, string key)
    {
        return GetAppValue(mainKey, subKey, key);
    }

    #region AppConfig
    public string Path => GetValue("FileSettings", "Path");
    public bool LogFiles => Convert.ToBoolean(GetValue("FileSettings", "LogFiles"));

    public string PayGateUri => GetValue("Integration", "PaygatePlus", "Uri");
    public string PayGateApi => GetValue("Integration", "PaygatePlus", "Api");
    public string PayGateSecret => GetValue("Integration", "PaygatePlus", "Secret");
    public string PayGateQueryUri => GetValue("Integration", "PaygatePlus", "QueryUri");
    public string PayGateTransctionsUri => GetValue("Integration", "PaygatePlus", "TransctionsUri");

    #endregion

}

