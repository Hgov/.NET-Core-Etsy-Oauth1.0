using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Concrete.Domain
{
    public class Country
    {
        public int country_id { get; set; }
        public string iso_country_code { get; set; }
        public string world_bank_country_code { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
