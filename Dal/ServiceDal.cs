
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dal.DataBase.Api;
using Dal.DataBase.Service;

using Dal.Json;
using Dal.Json.Api;
using Dal.DataBase.models;

namespace Dal//לעבור ולהבין
{
    public class ServiceDal : IDal
    {
        ServiceCollection col = new();
        public IStatoin StationService { get; }
        public IMeasurement MeasurementService { get; }

        public ServiceDal()
        {

            // הוספנו את מחלקות השרות שלנו לאוסף הסרויסים של השכבה
            col.AddSingleton<dbmanager>();
            col.AddScoped<IStatoin, ServiceStation>();
            col.AddScoped<IMeasurement, ServiceMeasurement>();
            // ספק שרותים
            ServiceProvider p = col.BuildServiceProvider();

            StationService = p.GetService<IStatoin>();

            MeasurementService = p.GetService<IMeasurement>();
        }
    }
}
