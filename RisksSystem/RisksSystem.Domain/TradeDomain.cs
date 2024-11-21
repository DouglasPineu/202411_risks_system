using RisksSystem.Domain.Interfaces;
using RisksSystem.Utils.Enums;

namespace RisksSystem.Domain
{
    public class TradeDomain : ITrade
    {
        public double Value { get; set; }

        public string ClientSector { get; set; }

        public DateTime NextPaymentDate { get; set; }

        public string? MsgError { get; set; }

        public long Line { get; set; }
        public RiskEnum Risk { get; set; }
    }
}
