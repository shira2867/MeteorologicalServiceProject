using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Json.Models
{
    public class Measurements
    {
        public int Id { get; set; }

        public string Time { get; set; }
        public double TrainFall { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
    }
}
