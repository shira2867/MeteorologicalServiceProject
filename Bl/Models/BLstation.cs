using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class BLstation
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Adress { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Principal { get; set; } = null!;
        public List<BlMeauserment> Meausers { get; set; }


    }
}
