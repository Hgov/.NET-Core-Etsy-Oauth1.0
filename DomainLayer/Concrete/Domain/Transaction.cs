using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Concrete.Domain
{

    public class Transaction
    {
        public long transaction_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int seller_user_id { get; set; }
        public int buyer_user_id { get; set; }
        public string creation_tsz { get; set; }
        public string paid_tsz { get; set; }
        public string shipped_tsz { get; set; }
        public string price { get; set; }
        public string currency_code { get; set; }
        public int quantity { get; set; }
        public long image_listing_id { get; set; }
        public long? receipt_id { get; set; }
        public string shipping_cost { get; set; }
        public bool is_digital { get; set; }
        public string file_data { get; set; }
        public long listing_id { get; set; }
        public bool is_quick_sale { get; set; }
        public long? seller_feedback_id { get; set; }
        public long? buyer_feedback_id { get; set; }
        public string transaction_type { get; set; }
        public string url { get; set; }

    }
}
