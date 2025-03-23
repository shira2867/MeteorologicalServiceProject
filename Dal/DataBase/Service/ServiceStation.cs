using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DataBase.Api;
using Dal.DataBase.models;



namespace Dal.DataBase.Service
{
    public class ServiceStation : IStatoin
    {

        dbmanager data;
        public ServiceStation(dbmanager d)
        {
            data = d;
        }
        public async Task< bool > Create1(GetCalculateDatum cal)//יצירת אוביקט מדידות של תחנה
        {
            try
            {
                var calculatedMeasurement = new GetCalculateDatum
                {
                    NameStation = cal.NameStation,
                    MaxTemp = cal.MaxTemp,
                    MinTemp = cal.MinTemp,
                    MaxRain = cal.MaxRain,
                    MinRain = cal.MinRain,
                };

                data.GetCalculateData.Add(calculatedMeasurement);
                data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("שגיאה בהוספת נתונים: " + ex.Message);
                return false;
            }
        }

        public void print(Station s)
        {
            Console.WriteLine("hello");
        }
        public  void Create(Station s)//הוספת תחנה בסיסית
        {
            if (data.Stations.Find(s.Id) == null)
            {
                

                data.Stations.Add(s);
                Console.WriteLine("add station");
                data.SaveChanges();
            }
        }
       
        public async Task< List<Station>> Get()//מחזיר רשימת תחנות בסיסית
        {
            return  data.Stations.ToList();

        }
        public async Task< List<GetCalculateDatum>> GetCalculateFullData()//מחזיר רשימת מדידות
        {
            return  data.GetCalculateData.ToList();

        }
        public List<Station> Search(Func<Station, bool> func)//חיפוש תחנה
        {
            return data.Stations.ToList().Where(s => func(s)).ToList();
        }

        public async Task< bool> Delete(Station s)//מחיקת תחנה
        {
            try
            {
                data.Stations.Remove(s);
                data.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task< bool> Update(Station s, string name)///עדכון תחנה לפי שם
        {
            try
            {
                // מחפש תחנה לפי שם
                Station station = data.Stations.FirstOrDefault(st => st.Name == s.Name);

                if (station == null)
                {
                    return false; // לא נמצאה תחנה כזו
                }

                // מעדכן את השם החדש
                station.Name = name;
                data.Stations.Update(station);
                data.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public List<Station> GetMeasure(string file_name)
        {
            throw new NotImplementedException();
        }

        public void GetById(string time, string file_name)
        {
            throw new NotImplementedException();
        }
    }
}
