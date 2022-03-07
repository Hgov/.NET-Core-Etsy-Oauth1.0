namespace DomainLayer.Concrete.Domain
{
    public class Price
    {
        public int amount { get; set; }
        public int divisor { get; set; }
        public string currency_code { get; set; }
        public string currency_formatted_short { get; set; }
        public string currency_formatted_long { get; set; }
        public string currency_formatted_raw { get; set; }
    }
}
