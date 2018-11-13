using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Contracts;
using System.Xml;
using System.Security.Principal;
using Manager;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using RBAC;
using System.IdentityModel.Policy;
using System.Threading;

namespace ServiceApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("Unesite broj porta za klijentsku komunikaciju: ");
            int brojPortaKlijent = Int32.Parse(Console.ReadLine());
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:" + brojPortaKlijent + "/WCFService";
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


            ServiceHost host = new ServiceHost(typeof(WCFService));
            host.AddServiceEndpoint(typeof(IWCFService), binding, address);

            //host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            //host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "serverSertifikat");

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();

            // Add a custom authorization policy to the service authorization behavior.
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

            try
            {
                host.Open();

                //string grupa = CertManager.GetOu(StoreName.My, StoreLocation.LocalMachine, "klijentSertifikat");
                //Console.WriteLine("Ovo je grupa " + grupa);
                Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
                //  Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }



            Thread t = new Thread(ConnectServer);
            t.Start();

            //Thread t2 = new Thread(ConnectClient);
            //t2.Start();

            //NetTcpBinding bindingServis = new NetTcpBinding();
            //bindingServis.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //string address1 = DBFunctions.LoadMyAddress();


            //string addressServis = "net.tcp://" + address1 + "/Update";

            //ServiceHost hostServis = new ServiceHost(typeof(Update));
            //hostServis.AddServiceEndpoint(typeof(IUpdate), bindingServis, addressServis);

            //hostServis.Open();

            //services = DBFunctions.CitanjeKonfiguracije();

            //Console.WriteLine("Nastavaka konektovanja servisa");
            //Console.ReadLine();

            //List<IUpdate> listOfWCF = new List<IUpdate>();

            //for (int i = 0; i < services.Count; i++)
            //{
            //    NetTcpBinding bindingIzListe = new NetTcpBinding();
            //    bindingIzListe.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //    string adresaIzListe = "net.tcp://" + services[i] + "/Update";

            //    IUpdate wCFService;

            //    ChannelFactory<IUpdate> channelFactory = new ChannelFactory<IUpdate>(bindingIzListe, adresaIzListe);

            //    wCFService = channelFactory.CreateChannel();
            //    listOfWCF.Add(wCFService);
            //}

            //Console.WriteLine("WCFService is opened. Press <enter> to finish...");

            //while (true)
            //{
            //    for (int i = 0; i < listOfWCF.Count; i++)
            //    {
            //        byte[] db1 = listOfWCF[i].IntegrityUdate();
            //        byte[] db2 = DBFunctions.MyDatabase();

            //        if (DBFunctions.CompareDataBases(db1, db2))
            //            Console.WriteLine("Iste.");
            //        else
            //            Console.WriteLine("Izmenio DB.");
            //    }

            //    //Console.WriteLine("Enter q if you want to exit:"); // Prompt
            //    //string line = Console.ReadLine(); // Get string from user
            //    //if (line == "q") // Check string
            //    //{
            //    //    break;
            //    //}

            //    Thread.Sleep(2000);
            //}

            //Console.ReadLine();

            //host.Close();
            //hostServis.Close();



            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            //Console.ReadLine();
            //host.Close();

            //hostServis.Close();

        }

        public static void ConnectClient()
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("Unesite broj porta za klijentsku komunikaciju: ");
            int brojPortaKlijent = Int32.Parse(Console.ReadLine());
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:" + brojPortaKlijent + "/WCFService";
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;




            ServiceHost host = new ServiceHost(typeof(WCFService));
            host.AddServiceEndpoint(typeof(IWCFService), binding, address);

            //host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            //host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "serverSertifikat");

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();

            // Add a custom authorization policy to the service authorization behavior.
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

            try
            {
                host.Open();

                //string grupa = CertManager.GetOu(StoreName.My, StoreLocation.LocalMachine, "klijentSertifikat");
                //Console.WriteLine("Ovo je grupa " + grupa);
                Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
                //  Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }

            //Console.ReadLine();
            //host.Close();
        }

        public static void ConnectServer()
        {
            List<string> services = new List<string>();
            NetTcpBinding bindingServis = new NetTcpBinding();
            bindingServis.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            string address1 = DBFunctions.LoadMyAddress();


            string addressServis = "net.tcp://" + address1 + "/Update";

            ServiceHost hostServis = new ServiceHost(typeof(Update));
            hostServis.AddServiceEndpoint(typeof(IUpdate), bindingServis, addressServis);

            hostServis.Open();

            services = DBFunctions.CitanjeKonfiguracije();

            Console.WriteLine("Nastavaka konektovanja servisa");
            Console.ReadLine();

            List<IUpdate> listOfWCF = new List<IUpdate>();
            List<ChannelFactory<IUpdate>> listOfChannelFactory = new List<ChannelFactory<IUpdate>>();

            for (int i = 0; i < services.Count; i++)
            {
                NetTcpBinding bindingIzListe = new NetTcpBinding();
                bindingIzListe.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                string adresaIzListe = "net.tcp://" + services[i] + "/Update";

                IUpdate wCFService;

                ChannelFactory<IUpdate> channelFactory = new ChannelFactory<IUpdate>(bindingIzListe, adresaIzListe);

                wCFService = channelFactory.CreateChannel();
                listOfChannelFactory.Add(channelFactory);
                listOfWCF.Add(wCFService);
            }

            Console.WriteLine("WCFService is opened. Press <enter> to finish...");

            while (true)
            {
                for (int i = 0; i < listOfWCF.Count; i++)
                {
                    try
                    {
                        byte[] db1 = listOfWCF[i].IntegrityUdate();
                        byte[] db2 = DBFunctions.MyDatabase();

                        if (DBFunctions.CompareDataBases(db1, db2))
                            Console.WriteLine("Iste.");
                        else
                            Console.WriteLine("Izmenio DB.");

                    }
                    catch (Exception e)
                    {
                        try
                        {

                            Console.WriteLine(e.Message);

                            NetTcpBinding bindingIzListe = new NetTcpBinding();
                            bindingIzListe.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                            string adresaIzListe = "net.tcp://" + services[i] + "/Update";

                            ChannelFactory<IUpdate> channelFactory = new ChannelFactory<IUpdate>(bindingIzListe, adresaIzListe);


                            //listOfWCF.RemoveAt(i);

                            listOfWCF[i] = channelFactory.CreateChannel();
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine(ee.Message);
                        }

                    }

                }

                //Console.WriteLine("Enter q if you want to exit:"); // Prompt
                //string line = Console.ReadLine(); // Get string from user
                //if (line == "q") // Check string
                //{
                //    break;
                //}

                Thread.Sleep(2000);
            }



            hostServis.Close();
        }

    }
}