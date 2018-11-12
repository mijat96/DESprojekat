using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contracts;
using System.Xml;
using Manager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace ClientApp
{
	public class WCFClient : ChannelFactory<IWCFService>, IWCFService, IDisposable
	{
		IWCFService factory;
        public WCFClient(NetTcpBinding binding, EndpointAddress address)
                    : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            //Console.WriteLine("CltCert: " + cltCertCN);


            //this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            //this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        #region Read()

        public bool Read()
        {
            bool allowed = false;
            try
            {
                allowed = factory.Read();

                if (allowed)
                    Console.WriteLine("Read() allowed.");
                else
                    Console.WriteLine("Read() not allowed.");
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to Read(). {0}", e.Message);
			}

            return allowed;
        }

        #endregion

        #region Edit()

        public bool Edit()
		{
            bool allowed = false;
            try
            {
                allowed = factory.Edit();

                if (allowed)
                    Console.WriteLine("Edit() allowed.");
                else
                    Console.WriteLine("Edit() not allowed.");
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to Edit(). {0}", e.Message);
			}

            return allowed;
		}

		#endregion		

		#region Add()
		
		public bool Add()
		{
            bool allowed = false;
			try
			{
				allowed = factory.Add();

                if (allowed)
    				Console.WriteLine("Add() allowed.");
                else
                    Console.WriteLine("Add() not allowed.");
            }
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to Add(). {0}", e.Message);
			}

            return allowed;
        }
        public bool Calculate()
        {
            bool allowed = false;
            try
            {
                allowed = factory.Calculate();

                if (allowed)
                    Console.WriteLine("Calculate() allowed.");
                else
                    Console.WriteLine("Calculate() not allowed.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Calculate(). {0}", e.Message);
            }

            return allowed;
        }
        public bool Delete()
        {
            bool allowed = false;
            try
            {
                allowed = factory.Delete();

                if (allowed)
                    Console.WriteLine("Delete() allowed.");
                else
                    Console.WriteLine("Delete() not allowed.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Delete(). {0}", e.Message);
            }

            return allowed;
        }

        #endregion

        #region EditSystemConfiguration



        #endregion

        public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}

        public List<Entity> LoadDataBase()
        {
            throw new NotImplementedException();
        }

        public void WriteInDatabase(Entity e)
        {
            throw new NotImplementedException();
        }

        public void WriteInDatabase(List<Entity> entities)
        {
            throw new NotImplementedException();
        }

        public void KonektujServise(string adresa)
        {
            throw new NotImplementedException();
        }
    }
}
