using Bl.Api;
using Bl.Models;
using System;
using System.Collections.Generic;

using Dal.DataBase.Api;
using Dal.DataBase.models;
using Dal.DataBase.Service;
using Dal;
using Dal.Json.Models;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Dal.DataBase.Service;
using System.Net.NetworkInformation;

namespace Bl.Service
{
    public class ServiceBlStation : IBlStation
    {
        public  IDal dal;  

        public ServiceBlStation(IDal dal)
        {
            this.dal = dal;
        }

        public async Task< List<BLstation>> GetFullData()//
        {
            List<BLstation> fullStation = new List<BLstation>();//יצירת רשימת תחנות עם מדידות

            var stations = await dal.StationService.Get();//שליפת רשימת התחנות מהדל
            foreach (var station in stations)
            {
                List<BlMeauserment> measurements = new List<BlMeauserment>();
                string fileName = station.Name;

                Console.WriteLine($"Trying to load measurements for station: {station.Name}");

                var m =await dal.MeasurementService.GetMeasure(fileName); // שליפת רשימת מדידות ממסד הנתונים 

                // בדוק אם התוצאה היא null או רשימה ריקה
                if (m == null )
                {
                    Console.WriteLine($" donttttt {station.Name}");
                }
                else
                {
                    foreach (var item in m)
                    {
                       

                        var measurement = item as Measurements; // ניסיון להמיר ל-Measurements
                        if (measurement == null)
                        {
                            Console.WriteLine("⚠️ שגיאה: אובייקט המדידה הוא NULL או מטיפוס לא נכון");
                            continue;
                        }

                        measurements.Add(new BlMeauserment
                        {
                            Id = measurement.Id,
                            Date = DateTime.Now,
                            Time = measurement.Time ?? "00:00",
                            TrainFall = measurement.TrainFall,
                            Temperature = measurement.Temperature,
                            WindSpeed = measurement.WindSpeed
                        });
                        Console.WriteLine("ok");
                        Console.WriteLine("fileName: " + fileName);
                    }
                }

                // יצירת תחנה עם המדידות והוספתה לרשימה
                BLstation bLstation = new BLstation()
                {
                    Name = station.Name,
                    Phone = station.Phone,
                    Principal = station.Principal,
                    Adress = station.Adress,
                    Meausers = measurements
                };

                fullStation.Add(bLstation);
            }
            //החזרת הרשימה המלאה
            return fullStation;
        }


       
            public  double MaxTemp(BLstation station)
            {
                double maxTemp = Double.MinValue;

              
                    foreach (var measurement in station.Meausers)
                    {
                        if (measurement.Temperature > maxTemp)
                        {
                            maxTemp = measurement.Temperature;
                        }
                    }
               

                return maxTemp;
            }
            public  double MinTemp(BLstation station)
            {
                double minTemp = Double.MaxValue;

              
                    foreach (var measurement in station.Meausers)
                    {
                        if (measurement.Temperature < minTemp)
                        {
                            minTemp = measurement.Temperature;
                        }
                    }
             

                return minTemp;
            }
            public  double MaxRain(BLstation station)
            {
                double MaxRain = Double.MinValue;

                
                    foreach (var measurement in station.Meausers)
                    {
                        if (measurement.TrainFall > MaxRain)
                        {
                            MaxRain = measurement.TrainFall;
                        }
                    }
               

                return MaxRain;
            }

            public  double MinRain(BLstation station)
            {
                double MinRain = Double.MaxValue;

                
                    foreach (var measurement in station.Meausers)
                    {
                        if (measurement.TrainFall < MinRain)
                        {
                            MinRain = measurement.TrainFall;
                        }
                    }
              

                return MinRain;
            }


        public async Task< List<GetCalculateData>> GetCalculate(List<BLstation> blStation)
        {
            List<GetCalculateData> getCalculateDatas = new List<GetCalculateData>();

            foreach (var station in blStation)
            {
                // חישוב הנתונים עבור התחנה הנוכחית בלבד
                GetCalculateData data = new GetCalculateData()
                {
                    NameStation = station.Name,
                    MaxTemp = MaxTemp(station),  
                    MinTemp = MinTemp(station),  
                    MinRain = MinRain(station),   
                    MaxRain = MaxRain(station)   
                };

                getCalculateDatas.Add(data);

                // המרת GetCalculateData ל-GetCalculateDatum
                var dalData = new GetCalculateDatum
                {
                    NameStation = data.NameStation,
                    MaxTemp = data.MaxTemp,
                    MinTemp = data.MinTemp,
                    MaxRain = data.MaxRain,
                    MinRain = data.MinRain
                };

                // הכנסת הנתונים לטבלה
                bool success = await dal.StationService.Create1(dalData);

                if (!success)
                {
                    Console.WriteLine("שגיאה בהכנסת הנתונים");
                }
            }
            return getCalculateDatas;
        }



        // יצירת תחנה חדשה
        public void CreateStation(Station s)
            {
                try
                {
                     dal.StationService.Create(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in BL while creating station: {ex.Message}");
                }
            }

            // עדכון תחנה
            public async Task< bool> UpdateStation(Station s,string n)
            {
                try
                {
                    return await dal.StationService.Update( s ,n);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in BL while updating station: {ex.Message}");
                    return false;
                }
            }

            // מחיקת תחנה
            public async Task< bool> DeleteStation(Station s)
            {
                try
                {
                    return await dal.StationService.Delete(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in BL while deleting station: {ex.Message}");
                    return false;
                }
            }

            // חיפוש תחנות לפי קריטריונים
            //public List<Station> SearchStations(Func<Station, bool> criteria)
            //{
            //    try
            //    {
            //        return dal.StationService.Search(criteria);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error in BLL while searching stations: {ex.Message}");
            //        return new List<Station>();
            //    }
            //}

            // קבלת כל התחנות

            public async Task< List<Station>> GetAllStations()
            {
                try
                {
                    return await dal.StationService.Get();//שליפת התחנות הבסיסיות
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in BL while getting all stations: {ex.Message}");
                    return new List<Station>();
                }
            }
        public  async Task< List<GetCalculateDatum>> GetFullD()
        {
            try
            {
                return await dal.StationService.GetCalculateFullData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BL while getting all stations: {ex.Message}");
                return new List<GetCalculateDatum>();
            }
        }


    }
    }







