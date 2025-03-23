
using Dal.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DataBase.Api;
using Dal.Json.Api;
    
namespace Dal
{
    public interface IDal
    {

        public IStatoin StationService { get; }
        public IMeasurement MeasurementService { get; }
    }
}
