using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using Manager;
using System.Threading;
using System.Security.Principal;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
            Console.WriteLine("Unesite broj porta: ");
            int brojPorta = Int32.Parse(Console.ReadLine());

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:" + brojPorta + "/WCFService";
            string srvCertCN = WindowsIdentity.GetCurrent().Name;
            srvCertCN = Formatter.ParseName(srvCertCN);
            Console.WriteLine(srvCertCN);
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, "serverSertifikat");
            EndpointAddress address2 = new EndpointAddress(new Uri(address),
                                      new X509CertificateEndpointIdentity(srvCert));
            //string grupa = CertManager.GetOu(StoreName.My, StoreLocation.LocalMachine, "klijentSertifikat");
            //Console.WriteLine("Ovo je grupa "+ grupa);
            int response;
            using (WCFClient proxy = new WCFClient(binding, address2))
			{
                do
                {
                    response = ClientMenu.Menu();
                    switch (response)
                    {
                        case 1:
                            proxy.Add();
                            break;
                        case 2:
                            proxy.Read();
                            break;
                        case 3:
                            proxy.Edit();
                            break;
                        case 4:
                            proxy.Delete();
                            break;
                        case 5:
                            proxy.Calculate();
                            break;
                        case 6:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Wrong command. Try agian");
                            break;

                    }
                } while (response != 6);
            }

			Console.ReadLine();
		}
	}
}
