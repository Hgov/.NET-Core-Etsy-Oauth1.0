using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Concrete.Domain
{
    public class Variation
    {
        public int variation_id { get; set; }
        public int property_id { get; set; }
        public long? value_id { get; set; }
        public string formatted_name { get; set; }
        public string formatted_value { get; set; }
    }
}
