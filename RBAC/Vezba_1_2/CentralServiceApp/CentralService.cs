using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace CentralServiceApp
{
    public class CentralService : ICentralService
    {
        public static List<int> ListaServisa = new List<int>();
        public void Connected()
        {
            var context = ServiceSecurityContext.Current;

            OperationContext var = OperationContext.Current;
            if (var != null)
            {
                MessageProperties message = var.IncomingMessageProperties;
                RemoteEndpointMessageProperty p = message[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                //ListaServisa.Add(p.Address);
                Console.WriteLine(p.Address);

            }
        }

        public bool IntegrityUpdate()
        {
            Console.WriteLine("uspesno");
            return false;
        }


    }
}
