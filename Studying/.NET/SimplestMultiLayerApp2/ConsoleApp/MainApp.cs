using System;
using BLL;
using System.Configuration;
using Ninject.Modules;
using Ninject;

namespace ConsoleApp
{
    class MainApp
    {
        static IKernel IoCExchange;
        static IKernel IoCDataReadWrite;

        static void Main(string[] args)
        {
            ConfigServices();
            RunServices();
            Console.ReadKey();
        }

        static void ConfigServices()
        {
            string path;
            path = ConfigurationManager.AppSettings["ratefile"];
            INinjectModule module = new BLLConfig(path);
            IoCExchange = new StandardKernel(new INinjectModule[] { new BLLConfig(path) });
            IoCExchange.Bind<IExchangeService>().To<CarrencyExchangeService>();
            
            path = ConfigurationManager.AppSettings["datafile"];
            IoCDataReadWrite = new StandardKernel(new INinjectModule[] { new BLLConfig(path) });
            IoCDataReadWrite.Bind(typeof(IDataReadWriteService<>)).To(typeof(ReadWriteService<>));
        }

        static void RunServices()
        {
            IExchangeService s1;
            IDataReadWriteService<int> s2;
            IDataReadWriteService<string> s3;

            //working with service s1
            s1 = IoCExchange.Get<IExchangeService>();
            CurrentRate rate = new CurrentRate() { Dollar = 28, Euro = 32 };
            s1.Rate = rate;
            Console.WriteLine($"Current rate in dollar - {s1.Rate.Dollar:0.00}, euro - {s1.Rate.Euro:0.00}");
            Console.Write("Input summa in hryvnia: ");
            string s = Console.ReadLine();
            decimal sum = Convert.ToDecimal(s);
            Console.WriteLine($"{sum} Hryvnia = {s1.HryvniaToDollar(sum):0.00} dollar");
            Console.WriteLine($"{sum} Hryvnia = {s1.HryvniaToEuro(sum):0.00} euro");


            //working with service s2
            s2 = IoCDataReadWrite.Get<IDataReadWriteService<int>>();
            s2.WriteData(12345);
            int number = s2.ReadData();
            Console.WriteLine($"number = {number}");

            //working with service s3
            s3 = IoCDataReadWrite.Get<IDataReadWriteService<string>>();
            s3.WriteData("Mama mila ramu");
            string str = s3.ReadData();
            Console.WriteLine($"str = {str}");
        }
    }
}