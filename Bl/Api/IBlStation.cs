using Bl.Models;
using Dal.DataBase.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bl.Api
{
    public interface IBlStation
    {
       Task< List<BLstation>> GetFullData();
      Task<  List<GetCalculateData>> GetCalculate(List<BLstation> blStation);
        public Task< List<Station>> GetAllStations();
        public Task< List<GetCalculateDatum>> GetFullD();
        //public List<Station> SearchStations(Func<Station, bool> criteria);

        public Task< bool> DeleteStation(Station s);
        public Task<bool> UpdateStation(Station s, string n);
        public void CreateStation(Station s);



    }
}
