using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidFeatures.Models
{
    internal class GeoLocationM
    {
        public int? id { get; set; }
        public Double latitude { get; set; }
        public Double longitude { get; set; }
        public Double? altitude { get; set; }
        public Double? accuracy { get; set; }
    }
}
