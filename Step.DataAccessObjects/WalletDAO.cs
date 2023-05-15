using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Step.Utils;
using Step.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Step.DataAccessObjects;

public class WalletDAO
{
    private readonly BaseDAO _baseDao;
    private readonly LogService _logger;

    public WalletDAO(BaseDAO baseDao, LogService logger)
    {
        _baseDao = baseDao;
        _logger = logger;
    }

    public string GetWallet(string id)
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
                objCmd.CommandText = "proc_get_wallet";
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
            _logger.LogError("GetWallet", "Npgsql error retriving Wallet", exc);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("GetWallet", "Error retriving Wallet", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public string GetWalletById(string id)
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
                objCmd.CommandText = "proc_get_wallet_details";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("v_wallet_id", NpgsqlDbType.Varchar).Value = id;
                objCmd.Parameters.Add("flag", NpgsqlDbType.Text).Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();


                // Get the values
                json = Convert.ToString(objCmd.Parameters["flag"].Value);
            }

            if (json == null)
                json = "{\"status\":\"Failed\",\"message\":\"Unexpected error occurred. Please try again later or contact your system administrator\"}";
        }

        catch (NpgsqlException exe)
        {
            _logger.LogError("GetWalletById", "Npgsql error retriving wallet details", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("GetWalletById", "Error retriving wallet details", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public string CreateWallet(WalletRequest model)
    {
        var json = string.Empty;
        //JObject joResponse;
        var connection = new NpgsqlConnection();

        // Open the connection.
        _baseDao.OpenConnection(connection);
        try
        {
            using (var cmdObj = new NpgsqlCommand())
            {
                cmdObj.Connection = connection;
                cmdObj.CommandText = "proc_create_wallet";
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters.Add("v_profile_id", NpgsqlDbType.Varchar).Value = model.ProfileId;
                cmdObj.Parameters.Add("v_country_code", NpgsqlDbType.Varchar).Value = model.CountryCode;
                cmdObj.Parameters.Add("v_account_no", NpgsqlDbType.Varchar).Value = model.AccountNumber;
                cmdObj.Parameters.Add("v_account_ref", NpgsqlDbType.Varchar).Value = model.AccountReference;
                cmdObj.Parameters.Add("v_account_name", NpgsqlDbType.Varchar).Value = model.AccountName;
                cmdObj.Parameters.Add("v_account_type", NpgsqlDbType.Varchar).Value = model.AccountType;
                cmdObj.Parameters.Add("v_bank_name", NpgsqlDbType.Varchar).Value = model.BankName;
                cmdObj.Parameters.Add("v_bank_code", NpgsqlDbType.Varchar).Value = model.BankCode;
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
            _logger.LogError("CreateWallet", "Npgsql error creating wallet", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("CreateWallet", "Error creating wallet", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public string UpdateWallet(WalletUpdateRequest model)
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
                cmdObj.CommandText = "proc_update_wallet";
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters.Add("v_wallet_id", NpgsqlDbType.Varchar).Value = model.WalletId;
                cmdObj.Parameters.Add("v_ststus_code", NpgsqlDbType.Varchar).Value = model.Status;
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
            _logger.LogError("UpdateWallet", "Npgsql error updating wallet details", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("UpdateWallet", "Error updating wallet details", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }
}

