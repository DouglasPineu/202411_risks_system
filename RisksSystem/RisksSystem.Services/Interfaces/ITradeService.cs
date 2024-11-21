using RisksSystem.Domain.Interfaces;

namespace RisksSystem.Services.Interfaces
{
    public interface ITradeService
    {
        ITrade GetTrade();
        ITrade ConvertData(string value, string cs, string nextPaymentsDate);
        ITrade SetRisk(DateTime dateReference, ITrade trade, int line);

        double ValidateValue(string value, ref string msg);

        string ValidateCS(string cs, ref string msg);

        DateTime ValidateDateFormat(string dateTest, ref string msg);
    }
}
