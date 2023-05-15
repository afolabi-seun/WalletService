using System;
using System.Runtime.CompilerServices;

namespace Step.Utils;

public class LogService
{
    private AppConfig _appConfig;

    public LogService(AppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    public void LogError(string main, string method, Exception ex)
    {

        var fileName = "errorLog";
        var strPath = _appConfig.Path;
        bool isLog = _appConfig.LogFiles;
        var exists = Directory.Exists(strPath);

        if(isLog)
        {
            if (!exists)
                Directory.CreateDirectory(strPath);

            var fullPath = strPath + fileName;
            var errorLogFile = Path.GetFullPath(fullPath);

            if (!File.Exists(errorLogFile))
            {
                File.Create(errorLogFile).Dispose();
            }
            using (var sw = File.AppendText(errorLogFile))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Class: " + main);
                sw.WriteLine("Error Type: " + method);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);
                sw.WriteLine(Environment.NewLine);
            }
        }
    }

    public void LogRequest(string request)
    {
        var fileName = "requestLog";
        var strPath = _appConfig.Path;
        bool isLog = _appConfig.LogFiles;
        var exists = Directory.Exists(strPath);

        if (isLog)
        {
            if (!exists)
                Directory.CreateDirectory(strPath);

            var fullPath = strPath + fileName;
            var requestLogFile = Path.GetFullPath(fullPath);

            if (!File.Exists(requestLogFile))
            {
                File.Create(requestLogFile).Dispose();
            }

            using (var sw = File.AppendText(requestLogFile))
            {
                sw.WriteLine("=============Request Log ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Log request: " + request);
                sw.WriteLine("===========End============= " + DateTime.Now);
                sw.WriteLine(Environment.NewLine);
            }
        }
    }
}

