using System;
using Ninject.Modules;
using DAL;
using DataProviderContract;

namespace BLL
{
    public class BLLConfig : NinjectModule
    {
        //private DALConfig _DALConfig;
        private string _con;

        public BLLConfig(string connection)
        {
            _con = connection;
           // _DALConfig = new DALConfig(connection);
        }
        
        public override void Load()
        {
            Bind(typeof(IDataProvider<>)).To(typeof(XMLDataProvider<>)).WithConstructorArgument(_con);
            //_DALConfig.Load();
            //foreach (var bind in _DALConfig.Bindings)
            //    AddBinding(bind);
            Bind(typeof(IDataContext<>)).To(typeof(DataContext<>));
            
        }

    }
}
