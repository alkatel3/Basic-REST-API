using System;
using DAL;
using System.IO;

namespace BLL
{
    public class CarrencyExchangeService : IExchangeService
    {
        private IDataContext<ExchangeRate> _dataContext;

        public CarrencyExchangeService(IDataContext<ExchangeRate> context)
        {
            _dataContext = context;
        }
        
        public CurrentRate Rate
        {
            get
            {
                try
                {
                    ExchangeRate er = _dataContext.GetData();
                    return new CurrentRate() { Dollar = er.Dollar, Euro = er.Euro };
                }
                catch (Exception ex)
                {
                    throw new RateException(ex.Message);
                }
            }
            set
            {
                try
                {
                    ExchangeRate er = new ExchangeRate() { Dollar = value.Dollar, Euro = value.Euro };
                    _dataContext.SetData(er);
                }
                catch (Exception ex)
                {
                    throw new RateException(ex.Message);
                }
            }
        }

        public decimal HryvniaToDollar(decimal sum)
        {
            return sum / Rate.Dollar;
        }

        public decimal HryvniaToEuro(decimal sum)
        {
            return sum / Rate.Euro;
        }

        public decimal DollarToHryvnia(decimal sum)
        {
            return sum * Rate.Dollar;
        }

        public decimal EuroToHryvnia(decimal sum)
        {
            return sum * Rate.Euro;
        }

    }
}
