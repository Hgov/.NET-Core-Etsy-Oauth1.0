namespace DomainLayer.Concrete.Domain
{
    public class Offering
    {
        public object offering_id { get; set; }
        public Price price { get; set; }
        public int quantity { get; set; }
        public int is_enabled { get; set; }
        public int is_deleted { get; set; }
    }
}
