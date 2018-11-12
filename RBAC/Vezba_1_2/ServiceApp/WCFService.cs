using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Xml;
using Contracts;
using Manager;
using RBAC;

namespace ServiceApp
{
	public class WCFService : IWCFService
	{
		public bool Read()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;

            var permission = Permissions.Read.ToString().ToLower();

            if (principal.IsInRole(permission))
            {
                Console.WriteLine("Read() successfully executed.");
                return true;
            }
            else
            {
                Console.WriteLine("Read() unsuccessfully executed.");
                return false;
            }
        }

        public bool Edit()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;

            var permission = Permissions.Edit.ToString().ToLower();

            if (principal.IsInRole(permission))
            {
                Console.WriteLine("Edit() successfully executed.");
                return true;
            }
            else
            {
                Console.WriteLine("Edit() unsuccessfully executed.");
                return false;
            }
        }		

		public bool Add()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;

            var permission = Permissions.Add.ToString().ToLower();

            if (principal.IsInRole(permission))
            {
                Console.WriteLine("Add() successfully executed.");
                return true;
            }
            else
            {
                Console.WriteLine("Add() unsuccessfully executed.");
                return false;
            }
        }

        public bool Delete()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;

            var permission = Permissions.Delete.ToString().ToLower();

            if (principal.IsInRole(permission))
            {
                Console.WriteLine("Delete() successfully executed.");
                return true;
            }
            else
            {
                Console.WriteLine("Delete() unsuccessfully executed.");
                return false;
            }
        }
        public bool Calculate()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;

            var permission = Permissions.Calculate.ToString().ToLower();

            if (principal.IsInRole(permission))
            {
                Console.WriteLine("Calculate() successfully executed.");

                return true;
            }
            else
            {
                Console.WriteLine("Calculate() unsuccessfully executed.");
                return false;
            }
        }

        public List<Entity> LoadDataBase()
        {
            List<Entity> ret = new List<Entity>();
            int count = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length;

            int id = 0;
            string region = "";
            string city = "";
            int year = 0;
            int cons = 0;

            using (XmlReader reader = XmlReader.Create(@"C:\Users\Administrator\Desktop\aaaa\Vezba_1_2\ServiceApp\bin\x64\Debug\DataBase" + count +".xml"))
            {
                while (reader.Read())
                {
                    if(reader.IsStartElement() && reader.Name.Equals("Entity"))
                    {
                        while (true)
                        {
                            if (reader.Name.Equals("Id"))
                            {
                                reader.Read();
                                int.TryParse(reader.Value, out id);
                                break;
                            }
                            else
                            {
                                reader.Read();
                            }
                        }

                        while(true)
                        { 

                            if(reader.Name.Equals("Region"))
                            {
                                reader.Read();
                                region = reader.Value;
                                break;
                            }
                            else
                            {
                                reader.Read();
                            }
                        }

                        while(true)
                        {
                            if(reader.Name.Equals("City"))
                            {
                                reader.Read();
                                city = reader.Value;
                                break;
                            }
                            else
                            {
                                reader.Read();
                            }
                        }

                        while(true)
                        {
                            if(reader.Name.Equals("Year"))
                            {
                                reader.Read();
                                int.TryParse(reader.Value, out year);
                                break;
                            }
                            else
                            {
                                reader.Read();
                            }
                        }

                        while(true)
                        {
                            if (reader.Name.Equals("ConsumptionOfElectricity"))
                            {
                                reader.Read();
                                int.TryParse(reader.Value, out cons);

                                Entity e = new Entity(id, region, city, year, cons);
                                ret.Add(e);
                                break;
                            }
                            else
                            {
                                reader.Read();
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public void WriteInDatabase(List<Entity> entities)
        {
            int count = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length;
            string filename = @"C:\Users\Administrator\Desktop\aaaa\Vezba_1_2\ServiceApp\bin\x64\Debug\DataBase" + count + ".xml";

            XmlWriter writer = null;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;

                writer = XmlWriter.Create(filename, settings);

                writer.WriteStartElement("Entities");

                foreach(Entity e in entities)
                {
                    writer.WriteStartElement("Entity");
                    writer.WriteElementString("Id", e.Id.ToString());
                    writer.WriteElementString("Region", e.Region);
                    writer.WriteElementString("City", e.City);
                    writer.WriteElementString("Year", e.Year.ToString());
                    writer.WriteElementString("ConsumptionOfElectricity", e.ConsuptionOfElectricity.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public void KonektujServise(string adresa)
        {
            NetTcpBinding netTcp = new NetTcpBinding();

            IWCFService wCFService;

            ChannelFactory<IWCFService> channelFactory = new ChannelFactory<IWCFService>();

            wCFService = channelFactory.CreateChannel();
        }
    }
}
