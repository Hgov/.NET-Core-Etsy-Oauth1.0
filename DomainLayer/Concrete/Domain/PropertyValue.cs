using System.Collections.Generic;

namespace DomainLayer.Concrete.Domain
{
    public class PropertyValue
    {
        public int property_id { get; set; }
        public string property_name { get; set; }
        public int? scale_id { get; set; }
        public string scale_name { get; set; }
        public List<string> values { get; set; }
        public List<object> value_ids { get; set; }
    }
}
