using Dal.Json.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Json.Api
{
    public interface IMeasurement : ICrud<Measurements>
    {
     Task<   IEnumerable <object>> GetMeasure(string fileName);
    }
}
