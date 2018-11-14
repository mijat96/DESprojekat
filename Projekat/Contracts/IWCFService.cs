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
        Entity Read(int id);

        [OperationContract]
        bool Edit(int monthNo, int monthlyConsumption, int idOfEntity);

        [OperationContract]
        bool Add(int id, string region, string city, int year, List<int> consumption);

        [OperationContract]
        double Calculate(string city);

        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        void WriteInDatabase(List<Entity> entities);

        [OperationContract]
        List<Entity> LoadDataBase();

        [OperationContract]
        void KonektujServise(string adresa);
    }
}
