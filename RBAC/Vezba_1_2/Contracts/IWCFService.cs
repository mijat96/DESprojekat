using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Contracts
{
	[ServiceContract]
	public interface IWCFService
	{
		[OperationContract]
		bool Read();

		[OperationContract]
        bool Edit();

		[OperationContract]
        bool Add();

        [OperationContract]
        bool Calculate();

        [OperationContract]
        bool Delete();

        [OperationContract]
        void WriteInDatabase(List<Entity> entities);

        [OperationContract]
        List<Entity> LoadDataBase();

        [OperationContract]
        void KonektujServise(string adresa);
	}
}
