using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CentralServiceApp
{
    class Program
    {
        //public static List<string> ListaServisa = new List<string>();
        static void Main(string[] args)
        {

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:8080/CentralService";

            ServiceHost host = new ServiceHost(typeof(CentralService));
            host.AddServiceEndpoint(typeof(ICentralService), binding, address);

            //host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            //host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            
            host.Open();

            int brojServisa = Int32.Parse(Console.ReadLine());
            string text = brojServisa.ToString();
            System.IO.File.WriteAllText(@"C:\Users\Administrator\Downloads\aaaa\Vezba_1_2\Broj.txt", text);
            Thread.Sleep(10000);

            for(int i = brojServisa; i > 0; i--)
            {
                NetTcpBinding bindingServis = new NetTcpBinding();
                string addressServis = "net.tcp://localhost:999" + i + "/WCFService";

                IWCFService factory;
                ChannelFactory<IWCFService> ChanelFactory = new ChannelFactory<IWCFService>(bindingServis, addressServis);

                factory = ChanelFactory.CreateChannel();
                factory.Read();

            }


            Console.WriteLine("Servis je otvoren " + host.State);

            //Thread myThread = new System.Threading.Thread(new System.Threading.ThreadStart(Conected));

            //myThread.Start();

            Console.ReadLine();
        }

        
    }
}
