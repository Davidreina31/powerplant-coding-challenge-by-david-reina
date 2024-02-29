using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Payload
    {
        public int Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<Powerplant> Powerplants { get; set; }
    }
}
