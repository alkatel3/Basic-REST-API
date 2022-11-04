using System;

namespace DAL
{
    [Serializable]
    public class ExchangeRate
    {
        public Decimal Dollar { get; set; }
        public Decimal Euro { get; set; }
    }
}
