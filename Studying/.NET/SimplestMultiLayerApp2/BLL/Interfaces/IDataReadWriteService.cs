using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDataReadWriteService<T>
    {
        T ReadData();
        void WriteData(T data);
    }
}
