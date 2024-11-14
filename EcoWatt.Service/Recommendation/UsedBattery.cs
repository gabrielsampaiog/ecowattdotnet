using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Service.Recommendation
{
    public class UsedBattery
    {
        public int IdUsuario { get; set; }
        public int IdBateria { get; set; }
        public float UseRelevance { get; set; }
    }
}
