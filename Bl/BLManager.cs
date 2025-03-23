using Bl.Api;
using Bl.Service;
using Bl.Models;
using Microsoft.Extensions.DependencyInjection;
using Dal.DataBase.Api;
using Dal.DataBase.Service;
using Dal.DataBase.models;
using Dal;
using System;

namespace Bl
{
    //מנהל את שכבת הביאל
    public class BLManager : IBl
    {
        public IBlStation StationBl { get; }

        public BLManager()
        {
            ServiceCollection services = new();

            // רישום נכון של השירותים
            services.AddScoped<IDal, ServiceDal>();
            services.AddScoped<IBlStation, ServiceBlStation>();

            ServiceProvider provider = services.BuildServiceProvider();

            // השתמש ב- GetRequiredService כדי למנוע קבלת null
            StationBl = provider.GetRequiredService<IBlStation>();
        }
    }
}
