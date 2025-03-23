
using Bl.Api;
using Bl.Service;
using Bl;
using Dal;
using Dal.DataBase.models;
using Dal.Json.Models;
using Bl.Models;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Station s = new Station( 1,"station1","jerusalem","0527635265","shira");
            //Station s1 = new Station(2, "station2", "Tel-Aviv", "052767777", "Naama");
            //Station s2 = new Station(3, "station3", "Tel-Aviv", "025867229", "moshe");
            //Station s3 = new Station(4, "station3", "Tel-Aviv", "0527853595", "liri");
            //IDal dal = new ServiceDal();

         
            //IBl bl = new BLManager();
            //dal.StationService.Create(s);
            //dal.StationService.Create(s1);
            //dal.StationService.Create(s2);
            //dal.StationService.Create(s3);
            //dal.StationService.Update(s3, "station4");
            //IBl blManager = new BLManager();


            //// קריאה לפונקציה GetFullData דרך ה- IBllStation
            //List<BLstation> stationsData = blManager.Stations.GetFullData();

            //// הצגת התוצאות (לפי הצורך)
            //foreach (var station in stationsData)
            //{
            //    Console.WriteLine($"Station Name: {station.Name}");
            //    Console.WriteLine($"Phone: {station.Phone}");
            //    Console.WriteLine($"Principal: {station.Principal}");
            //    Console.WriteLine($"Address: {station.Adress}");
            //    Console.WriteLine("Measurements:");

            //    foreach (var measurement in station.Meausers)
            //    {
            //        Console.WriteLine($"  Time: {measurement.Time}, Temperature: {measurement.Temperature}, Rainfall: {measurement.TrainFall}");
            //    }

            //    Console.WriteLine();
            //}
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(" getCalculateDatas");

            //List<GetCalculateData> getCalculateDatas = blManager.Stations.GetCalculate(stationsData);
            //foreach (var data in getCalculateDatas)
            //{
            //    Console.WriteLine($"Station: {data.NameStation}");
            //    Console.WriteLine($"Max Temperature: {data.MaxTemp}");
            //    Console.WriteLine($"Min Temperature: {data.MinTemp}");
            //    Console.WriteLine($"Max Rainfall: {data.MaxRain}");
            //    Console.WriteLine($"Min Rainfall: {data.MinRain}");
            //    Console.WriteLine(new string('-', 30)); // קו מפריד
            //}

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IBl,BLManager>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy => policy.WithOrigins("http://localhost:3000") // הכתובת של React
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
