using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Step.Utils;
using Step.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml.Linq;
using Step.ValueObjects.PayPlus.WebHook;
using System.Net.NetworkInformation;

namespace Step.DataAccessObjects;

public class TransactionDAO
{
    private readonly BaseDAO _baseDao;
    private readonly LogService _logger;

    public TransactionDAO(BaseDAO baseDao, LogService logger)
    {
        _baseDao = baseDao;
        _logger = logger;
    }

    public string GetTransaction(string id)
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
                objCmd.CommandText = "proc_get_tnx";
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
            _logger.LogError("GetTransaction", "Npgsql error retriving transaction(s)", exc);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("GetTransaction", "Error retriving transaction(s)", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public string GetTransactionById(string id)
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
                objCmd.CommandText = "proc_get_tnx_details";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("v_ref_id", NpgsqlDbType.Varchar).Value = id;
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
            _logger.LogError("GetTransactionById", "Npgsql error retriving transaction details", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("GetTransactionById", "Error rretriving transaction details", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public string CreateTransaction(TransactionRequest model)
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
                cmdObj.CommandText = "proc_create_txn";
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters.Add("v_txn_ref", NpgsqlDbType.Varchar).Value = model.TransactionRef;
                cmdObj.Parameters.Add("v_from_wallet_num", NpgsqlDbType.Varchar).Value = model.FromCardNumber;
                cmdObj.Parameters.Add("v_to_wallet_num", NpgsqlDbType.Varchar).Value = model.ToCardNumber;
                cmdObj.Parameters.Add("v_amount", NpgsqlDbType.Integer).Value = model.TotalAmount;
                cmdObj.Parameters.Add("v_txn_fee", NpgsqlDbType.Integer).Value = model.TransactionFee;
                cmdObj.Parameters.Add("v_conv_fee", NpgsqlDbType.Integer).Value = model.ConvenienceFee;
                cmdObj.Parameters.Add("v_description", NpgsqlDbType.Varchar).Value = model.Description;
                cmdObj.Parameters.Add("v_txn_type", NpgsqlDbType.Varchar).Value = model.TransactionType ?? "";
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
            _logger.LogError("CreateTransaction", "Npgsql error creating transaction", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("CreateTransaction", "Error creating transaction", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }

    public void CreateWbbHookTransaction(FidelityWebHookRequest model)
    {
        var connection = new NpgsqlConnection();
        // Open the connection.
        _baseDao.OpenConnection(connection);
        try
        {
            using (var cmdObj = new NpgsqlCommand())
            {
                cmdObj.Connection = connection;
                cmdObj.CommandText = "proc_create_txn_fdlty";
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters.Add("v_req_ref", NpgsqlDbType.Varchar).Value = model.Request_Ref;
                cmdObj.Parameters.Add("v_req_type", NpgsqlDbType.Varchar).Value = model.Request_Type;
                cmdObj.Parameters.Add("v_requester", NpgsqlDbType.Varchar).Value = model.Requester;
                cmdObj.Parameters.Add("v_status", NpgsqlDbType.Varchar).Value = model.Details?.Status;
                cmdObj.Parameters.Add("v_provider", NpgsqlDbType.Varchar).Value = model.Details?.Provider;
                cmdObj.Parameters.Add("v_trx_ref", NpgsqlDbType.Varchar).Value = model.Details?.Transaction_Ref;
                cmdObj.Parameters.Add("v_trx_desc", NpgsqlDbType.Varchar).Value = model.Details?.Transaction_Desc;
                cmdObj.Parameters.Add("v_trx_type", NpgsqlDbType.Varchar).Value = model.Details?.Transaction_Type;
                cmdObj.Parameters.Add("v_cust_ref", NpgsqlDbType.Varchar).Value = model.Details?.Customer_Ref;
                cmdObj.Parameters.Add("v_cust_email", NpgsqlDbType.Varchar).Value = model.Details?.Customer_Email;
                cmdObj.Parameters.Add("v_cust_surname", NpgsqlDbType.Varchar).Value = model.Details?.Customer_Surname;
                cmdObj.Parameters.Add("v_cust_fname", NpgsqlDbType.Varchar).Value = model.Details?.Customer_Firstname;
                cmdObj.Parameters.Add("v_cust_mobile_no", NpgsqlDbType.Varchar).Value = model.Details?.Customer_Mobile_No;
                cmdObj.Parameters.Add("v_tag", NpgsqlDbType.Varchar).Value = model.Details?.Data?.Tag;
                double amt = 0; var str_amt = model.Details?.Data?.Amount;
                if (str_amt != null)
                    amt = double.Parse(str_amt, CultureInfo.InvariantCulture);
                cmdObj.Parameters.Add("v_amt", NpgsqlDbType.Double).Value = amt;
                cmdObj.Parameters.Add("v_from_bnk_code", NpgsqlDbType.Varchar).Value = model.Details?.Data?.BankCode;
                cmdObj.Parameters.Add("v_bnk_name", NpgsqlDbType.Varchar).Value = model.Details?.Data?.BankName;
                //cmdObj.Parameters.Add("v_txn_date", NpgsqlDbType.Date).Value = model.Details?.Data?.TranDate.ToShortDateString();
                cmdObj.Parameters.Add("v_bnk_code", NpgsqlDbType.Varchar).Value = model.Details?.Data?.Bank_Code;
                cmdObj.Parameters.Add("v_cr_account", NpgsqlDbType.Varchar).Value = model.Details?.Data?.CrAccount;
                //cmdObj.Parameters.Add("v_create_date", NpgsqlDbType.Date).Value = model.Details?.Data?.CreatedOn.ToShortDateString();
                cmdObj.Parameters.Add("v_narration", NpgsqlDbType.Varchar).Value = model.Details?.Data?.Narration;
                cmdObj.Parameters.Add("v_session_id", NpgsqlDbType.Varchar).Value = model.Details?.Data?.SessionId;
                cmdObj.Parameters.Add("v_channel_code", NpgsqlDbType.Varchar).Value = model.Details?.Data?.ChannelCode;
                int charg = 0; var str_charg = model.Details?.Data?.ChargeAmount;
                if (str_charg != null)
                    charg = int.Parse(str_charg, CultureInfo.InvariantCulture);
                cmdObj.Parameters.Add("v_cr_acc_name", NpgsqlDbType.Varchar).Value = model.Details?.Data?.CrAccountName;
                cmdObj.Parameters.Add("v_orign_bvn", NpgsqlDbType.Varchar).Value = model.Details?.Data?.OriginatorBvn ?? "";
                cmdObj.Parameters.Add("v_acc_no", NpgsqlDbType.Varchar).Value = model.Details?.Data?.Account_Number;
                cmdObj.Parameters.Add("v_benef_bvn", NpgsqlDbType.Varchar).Value = model.Details?.Data?.BeneficiaryBvn ?? "";
                cmdObj.Parameters.Add("v_enq_ref_name", NpgsqlDbType.Varchar).Value = model.Details?.Data?.NameEnquiryRef;
                cmdObj.Parameters.Add("v_orign_name", NpgsqlDbType.Varchar).Value = model.Details?.Data?.OriginatorName;
                cmdObj.Parameters.Add("v_pay_ref", NpgsqlDbType.Varchar).Value = model.Details?.Data?.PaymentReference;
                cmdObj.Parameters.Add("v_orign_cbn_code", NpgsqlDbType.Varchar).Value = model.Details?.Data?.OriginatorCbnCode;
                cmdObj.Parameters.Add("v_orign_kyc_level", NpgsqlDbType.Varchar).Value = model.Details?.Data?.OriginatorKycLevel ?? "";
                cmdObj.Parameters.Add("v_ben_kyc_level", NpgsqlDbType.Varchar).Value = model.Details?.Data?.BeneficiaryKycLevel ?? "";
                cmdObj.Parameters.Add("v_tran_loc", NpgsqlDbType.Varchar).Value = model.Details?.Data?.TransactionLocation;
                cmdObj.Parameters.Add("v_coll_acc_no", NpgsqlDbType.Varchar).Value = model.Details?.Data?.CollectionAccountNumber;
                cmdObj.Parameters.Add("v_orign_acc_no", NpgsqlDbType.Varchar).Value = model.Details?.Data?.OriginatorAccountNumber;
                cmdObj.Parameters.Add("v_dest_inst_bank_code", NpgsqlDbType.Varchar).Value = model.Details?.Data?.DestinationInstitutionBankCode;

                cmdObj.ExecuteNonQuery();
            }
        }
        catch (NpgsqlException exe)
        {
            _logger.LogError("CreateWbbHookTransaction", "Npgsql error creating transaction", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("CreateWbbHookTransaction", "Error creating transaction", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public string CreatCashOut(CashOutRequest model)
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
                cmdObj.CommandText = "proc_create_txn_disburse";
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters.Add("v_profile_id", NpgsqlDbType.Varchar).Value = model.ProfileId;
                cmdObj.Parameters.Add("v_cust_ref", NpgsqlDbType.Varchar).Value = model.CustomerRef;
                cmdObj.Parameters.Add("v_from_wallet_num", NpgsqlDbType.Varchar).Value = model.FromCardNumber;
                cmdObj.Parameters.Add("v_tnx_amt", NpgsqlDbType.Integer).Value = model.Amount;
                cmdObj.Parameters.Add("v_txn_fee", NpgsqlDbType.Integer).Value = model.TransactionFee;
                cmdObj.Parameters.Add("v_conv_fee", NpgsqlDbType.Integer).Value = model.ConvenienceFee;
                cmdObj.Parameters.Add("v_pay_id", NpgsqlDbType.Varchar).Value = model.PaymentId ?? "";
                cmdObj.Parameters.Add("v_txn_ref", NpgsqlDbType.Varchar).Value = model.Reference ?? "";
                cmdObj.Parameters.Add("v_status", NpgsqlDbType.Varchar).Value = model.Status ?? "";
                cmdObj.Parameters.Add("v_message", NpgsqlDbType.Varchar).Value = model.Message ?? "";
                cmdObj.Parameters.Add("v_narration", NpgsqlDbType.Varchar).Value = model.Narration ?? "";
                cmdObj.Parameters.Add("v_prov_resp_code", NpgsqlDbType.Varchar).Value = model.ProviderResponseCode ?? "";
                cmdObj.Parameters.Add("v_tnx_final_amt", NpgsqlDbType.Integer).Value = model.TransactionFinalAmount;
                cmdObj.Parameters.Add("v_orgin_acc_name", NpgsqlDbType.Varchar).Value = model.OriginatorAccountName ?? "";
                cmdObj.Parameters.Add("v_orgin_acc_no", NpgsqlDbType.Varchar).Value = model.OriginatorAccountNumber ?? "";
                cmdObj.Parameters.Add("v_ben_acc_no", NpgsqlDbType.Varchar).Value = model.BeneficiaryAccountNumber ?? "";
                cmdObj.Parameters.Add("v_ben_acc_name", NpgsqlDbType.Varchar).Value = model.BeneficiaryAccountName ?? "";
                cmdObj.Parameters.Add("v_den_inst_code", NpgsqlDbType.Varchar).Value = model.DestinationInstitutionCode ?? "";
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
            _logger.LogError("CreatCashOut", "Npgsql error creating cashout transaction", exe);
            throw;
        }
        catch (Exception exc)
        {
            _logger.LogError("CreatCashOut", "Error creating cashout transaction", exc);
            throw;
        }
        finally
        {
            connection.Close();
        }

        return json;
    }
}

