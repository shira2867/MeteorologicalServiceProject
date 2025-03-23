using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dal;
using Dal.DataBase.models;
using Dal.Json.Api;
using Dal.Json.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dal.Json
{
    public class ServiceMeasurement : IMeasurement
    {

        dbmanager data;
        public ServiceMeasurement(dbmanager d)
        {
            data = d;
        }


        public async Task< List<Measurements> >GetMeasure(string fileName)
        {
            List<Measurements> measurements = new List<Measurements>();

            // בונה נתיב מלא עם התיקייה והסיומת
            string directoryPath = @"C:\Users\משתמש\Desktop\measureStation\Dal\Json\JsonesFiles";
            string fileNameWithExtension = fileName.EndsWith(".json") ? fileName : fileName + ".json";
            string fullFilePath = Path.Combine(directoryPath, fileNameWithExtension);

            Console.WriteLine($"Checking file path: {fullFilePath}");

            if (!File.Exists(fullFilePath))
            {
                Console.WriteLine("⚠️ הקובץ לא נמצא: " + fullFilePath);
                return measurements; // מחזיר רשימה ריקה כדי למנוע שגיאות
            }

            try
            {
                // קריאת תוכן הקובץ JSON
                string content = File.ReadAllText(fullFilePath);
                Console.WriteLine("📄 JSON Loaded: " + content.Substring(0, Math.Min(content.Length, 200))); // מדפיס חלק מהתוכן

                // פענוח JSON
                using JsonDocument jsonDoc = JsonDocument.Parse(content);

                // בדיקה אם המפתח "measurements" קיים
                if (!jsonDoc.RootElement.TryGetProperty("measurements", out JsonElement measurementsArray))
                {
                    Console.WriteLine("⚠️ מפתח 'measurements' חסר ב-JSON");
                    return measurements;
                }

                foreach (var item in measurementsArray.EnumerateArray())
                {
                    try
                    {
                        Measurements measurement = new Measurements
                        {
                            Id = item.GetProperty("id").GetInt32(),
                            Time = item.GetProperty("time").GetString(),
                            TrainFall = item.GetProperty("rainfall").GetDouble(),
                            Temperature = item.GetProperty("temperature").GetDouble(),
                            WindSpeed = item.GetProperty("wind_speed").GetDouble()
                        };
                        measurements.Add(measurement);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("⚠️ שגיאה בפענוח מדידה אחת: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ שגיאה בעת קריאת הקובץ: " + ex.Message);
            }

            return measurements;
        }


        public void GetById(string time, string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    // קריאת תוכן הקובץ JSON
                    string content = File.ReadAllText(fileName);

                    // פענוח JSON לאובייקט
                    var jsonDoc = JsonDocument.Parse(content);

                    // שליפת רשימת המדידות מתוך JSON
                    var measurementsArray = jsonDoc.RootElement
                        .GetProperty("measurements")
                        .EnumerateArray();

                    foreach (var item in measurementsArray)
                    {
                        if (item.GetProperty("time").GetString() == time)
                        {

                            Console.WriteLine("the temperature at" + item.GetProperty("time").GetString() + " is " + item.GetProperty("temperature").GetString());


                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"שגיאה בעת קריאת הקובץ: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("הקובץ לא נמצא");
            }
        }
        

        public List<Measurements> GetAll()
        {
            throw new NotImplementedException();
        }

        public Measurements GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Measurements item)
        {
            throw new NotImplementedException();
        }

        public void Update(Measurements item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

       async Task< IEnumerable<object> >IMeasurement.GetMeasure(string fileName)
        {
            // קריאה לפונקציה שלך (GetMeasure)
            List<Measurements> measurements = await GetMeasure(fileName);

            // החזרת רשימה של אובייקטים (הפיכת List<Measurements> ל- IEnumerable<object>)
            return measurements.Cast<object>();
        }

    }
}
