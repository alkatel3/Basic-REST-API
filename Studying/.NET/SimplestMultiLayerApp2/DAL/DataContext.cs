using System;
using DataProviderContract;

namespace DAL
{
    public class DataContext<T>: IDataContext<T>
    {
        private IDataProvider<T> _dataProvider;

        public DataContext(IDataProvider<T> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public T GetData()
        {
            if (_dataProvider != null)
            {
                if (_storedData != null)
                    return _storedData;
                else
                {
                    try
                    {
                        _storedData = _dataProvider.Read();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    return _storedData;
                }
            }
            else
                throw new InvalidOperationException("Data provider is undefined");
        }

        public void SetData(T data)
        {
            if (_dataProvider != null)
            {
                _dataProvider.Write(data);
                _storedData = data;
            }
            else
                throw new InvalidOperationException("Data provider is undefined");
        }

        private T _storedData;
    }

}

