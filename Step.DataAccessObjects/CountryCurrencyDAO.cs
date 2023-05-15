using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Step.Utils;
using Step.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Step.DataAccessObjects;

public class CountryCurrencyDAO
{
    private readonly BaseDAO _baseDao;
    private readonly LogService _logger;

    public CountryCurrencyDAO(BaseDAO baseDao, LogService logger)
    {
        _baseDao = baseDao;
        _logger = logger;
    }

    public string GetCountryCurrency(string id)
    {
        var json = string.Empty;
        var connection = new NpgsqlConnection();

        // Open the connection.
        _baseDao.OpenConnection(connection);

        try
        {
            using (var objCmd = new NpgsqlCommand())
            {
                objCmd.Connection = connection;
                objCmd.CommandText = "proc_get_country_currency";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("v_profile_id", NpgsqlDbType.Varchar).Value = id;
                objCmd.Parameters.Add("flag", NpgsqlDbType.Text).Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();

                // Get the values
                json = Convert.ToString(objCmd.Parameters["flag"].Value);
            }

            if (json == null)
                json = "{\"status\":\"Failed\",\"message\":\"Unexpected error occurred. Please try again later or contact your system administrator\"}";
        }
        catch (NpgsqlException exc)
        {
            _logger.LogError("GetCountryCurrency", "Npgsql error retriving countryCurrency", exc);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("GetCountryCurrency", "Error retriving countryCurrency", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public string CreateCountryCurrency(CountryCurrencyRequest model)
    {
        var json = string.Empty;
        var connection = new NpgsqlConnection();

        // Open the connection.
        _baseDao.OpenConnection(connection);
        try
        {
            using (var cmdObj = new NpgsqlCommand())
            {
                cmdObj.Connection = connection;
                cmdObj.CommandText = "proc_create_country_currency";
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters.Add("v_profile_id", NpgsqlDbType.Varchar).Value = model.ProfileId;
                cmdObj.Parameters.Add("v_country_name", NpgsqlDbType.Varchar).Value = model.CountryName;
                cmdObj.Parameters.Add("v_country_code", NpgsqlDbType.Varchar).Value = model.CountryCode;
                cmdObj.Parameters.Add("v_currency_code", NpgsqlDbType.Varchar).Value = model.CurrencyCode;
                cmdObj.Parameters.Add("flag", NpgsqlDbType.Text).Direction = ParameterDirection.Output;

                cmdObj.ExecuteNonQuery();

                // Get the values
                json = Convert.ToString(cmdObj.Parameters["flag"].Value);
            }

            if (json == null)
                json = "{\"status\":\"Failed\",\"message\":\"Unexpected error occurred. Please try again later or contact your system administrator\"}";
        }
        catch (NpgsqlException exe)
        {
            _logger.LogError("CreateCountryCurrency", "Npgsql error creating countryCurrency", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("CreateCountryCurrency", "Error creating countryCurrency", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

}

