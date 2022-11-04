using System;
using System.IO;
using System.Xml.Serialization;
using DataProviderContract;

namespace DAL
{
    public class XMLDataProvider<T> : IDataProvider<T>
    {
        private string _connectString;

        public XMLDataProvider(string connection)
        {
            _connectString = connection;
        }
        public T Read()
        {
            T data;
            using (FileStream fs = new FileStream(_connectString, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                try
                {
                    data = (T)formatter.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return data;
        }

        public void Write(T data)
        {
            using (FileStream fs = new FileStream(_connectString, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(data.GetType());
                try
                {
                    formatter.Serialize(fs, data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
