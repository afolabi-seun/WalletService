using System;
using Npgsql;
using Step.Utils;
using Microsoft.Extensions.Logging;

namespace Step.DataAccessObjects;

public class BaseDAO
{
    private AppConfig _appConfig;

    public BaseDAO(AppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    #region Connection Core

    private string GetStepConnectionString()
    {
        return _appConfig.GeConnectionString()!;
    }

    public void OpenConnection(NpgsqlConnection connection)
    {
        connection.ConnectionString = GetStepConnectionString();
        connection.Open();
    }

    #endregion Connection Core
}

