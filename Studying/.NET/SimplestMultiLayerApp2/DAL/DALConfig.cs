using System;
using Ninject.Modules;
using DataProviderContract;

namespace DAL
{
    
    public class DALConfig : NinjectModule
    {
        private string _connectionString;

        public DALConfig(string connection)
        {
            _connectionString = connection;
        }

        public override void Load()
        {
            Bind(typeof(IDataProvider<>)).To(typeof(XMLDataProvider<>)).WithConstructorArgument(_connectionString);
            
        }
    }
}
