using System;
using DAL;

namespace BLL
{
    public class ReadWriteService<T>:IDataReadWriteService<T>
    {
        private IDataContext<T> _dataContext;

        public ReadWriteService(IDataContext<T> context)
        {
            _dataContext = context;
        }

        public T ReadData()
        {
            return _dataContext.GetData();
        }

        public void WriteData(T data)
        {
            _dataContext.SetData(data);
        }

    }
}
