using System.Data;
using Step.DataAccessObjects;
using Step.ValueObjects;
using Step.ValueObjects.PayPlus.WebHook;

namespace Step.BusinessServices;
public class ConfigService
{
    private readonly FeesDAO _feesDAO;
    private readonly WalletDAO _walletDao;
    private readonly TransactionDAO _transactionDao;
    private readonly CountryCurrencyDAO _countryCurrencyDao;

    public ConfigService(FeesDAO feesDAO, WalletDAO walletDao, TransactionDAO transactionDao, CountryCurrencyDAO countryCurrencyDao)
    {
        _feesDAO = feesDAO;
        _walletDao = walletDao;
        _transactionDao = transactionDao;
        _countryCurrencyDao = countryCurrencyDao;
    }

    #region Wallet

    public string RetrieveWallet(string id)
    {
        return _walletDao.GetWallet(id);
    }
    public string RetrieveWalletById(string id)
    {
        return _walletDao.GetWalletById(id);
    }
    public string CreateWallet(WalletRequest model)
    {
        return _walletDao.CreateWallet(model);
    }
    public string UpdateWalletById(WalletUpdateRequest model)
    {
        return _walletDao.UpdateWallet(model);
    }
    #endregion Wallet

    #region Transaction

    public string RetrieveTransaction(string id)
    {
        return _transactionDao.GetTransaction(id);
    }
    public string RetrieveTransactionByTxnId(string id)
    {
        return _transactionDao.GetTransactionById(id);
    }
    public string CreateTransaction(TransactionRequest model)
    {
        return _transactionDao.CreateTransaction(model);
    }
    public void CreateFdltyWbbHookTransaction(FidelityWebHookRequest model)
    {
        _transactionDao.CreateWbbHookTransaction(model);
    }
    public string CashOut(CashOutRequest model)
    {
        return _transactionDao.CreatCashOut(model);
    }
    #endregion Transaction

    #region CountryCurrency

    public string RetrieveCountryCurrency(string id)
    {
        return _countryCurrencyDao.GetCountryCurrency(id);
    }
    public string CreateCountryCurrency(CountryCurrencyRequest model)
    {
        return _countryCurrencyDao.CreateCountryCurrency(model);
    }
    #endregion CountryCurrency

    #region Fees

    public string RetrieveFees(string id)
    {
        return _feesDAO.GetFees(id);
    }
    public string CreateFees(FeeRequest model)
    {
        return _feesDAO.CreateFees(model);
    }
    #endregion CountryCurrency
}

