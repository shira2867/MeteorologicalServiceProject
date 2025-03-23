using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DataBase.models;
    

namespace Bl.Api
{
    public interface IBl 
    {
        public IBlStation StationBl{ get; }
    }
}

