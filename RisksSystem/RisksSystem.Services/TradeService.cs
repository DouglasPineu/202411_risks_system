using RisksSystem.Domain;
using RisksSystem.Domain.Interfaces;
using RisksSystem.Services.Interfaces;
using RisksSystem.Utils.Enums;
using RisksSystem.Utils.Extensions;
using System.Diagnostics;
using System.Globalization;

namespace RisksSystem.Services
{
    public class TradeService : ITradeService
    {
        public ITrade GetTrade()
        {
            return new TradeDomain();
        }

        public ITrade ConvertData(string value, string cs, string nextPaymentsDate)
        {
            TradeDomain tradeDomain = new TradeDomain();
            string strMsg = string.Empty;
            tradeDomain.Value = this.ValidateValue(value, ref strMsg);
            tradeDomain.ClientSector = this.ValidateCS(cs, ref strMsg);
            tradeDomain.NextPaymentDate = this.ValidateDateFormat(nextPaymentsDate, ref strMsg);
            tradeDomain.MsgError = strMsg;

            return tradeDomain;
        }

        public ITrade SetRisk(DateTime dateReference, ITrade trade, int line)
        {
            TradeDomain tradeDomain = (TradeDomain)trade;

            tradeDomain.Line = line;

            if ((trade.NextPaymentDate - dateReference).TotalDays < 30)
                tradeDomain.Risk = RiskEnum.EXPIRED;
            else if (trade.ClientSector == "Private" && trade.Value >= 1000000)
                tradeDomain.Risk = RiskEnum.HIGHRISK;
            else if (trade.ClientSector == "Public" && trade.Value >= 1000000)
                tradeDomain.Risk = RiskEnum.MEDIUMRISK;
            else
                tradeDomain.Risk = RiskEnum.NORISK;

            return tradeDomain;
        }



        public double ValidateValue(string value, ref string msg)
        {
            bool valid = false;
            double v = value.ConvertToDouble(out valid);

            if (!valid)
                msg += "; Value incorrect. Format correct ex.: 1000.59";

            return v;
        }

        public string ValidateCS(string cs, ref string msg)
        {
            string[] arrValidCS = new string[] { "Public", "Private" };

            if (!arrValidCS.Contains(cs))
                msg += "; Category not exists";

            return cs;
        }

        public DateTime ValidateDateFormat(string dateTest, ref string msg)
        {
            bool valid = false;
            DateTime date = dateTest.ConvertToDate(out valid);

            if (!valid)
                msg = "; Date incorrect. Format correct ex.: month/day/year.";

            return date;
        }
    }
}
