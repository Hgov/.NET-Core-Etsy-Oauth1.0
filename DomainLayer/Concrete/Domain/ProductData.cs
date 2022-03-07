using System.Collections.Generic;

namespace DomainLayer.Concrete.Domain
{
    public class ProductData
    {
        public object product_id { get; set; }
        public string sku { get; set; }
        public List<PropertyValue> property_values { get; set; }
        public List<Offering> offerings { get; set; }
        public int is_deleted { get; set; }
    }
}
