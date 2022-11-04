using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IExchangeService
    {
        CurrentRate Rate { get; set; }
        decimal HryvniaToDollar(decimal sum);
        decimal HryvniaToEuro(decimal sum);
        decimal DollarToHryvnia(decimal sum);
        decimal EuroToHryvnia(decimal sum);
    }
}
