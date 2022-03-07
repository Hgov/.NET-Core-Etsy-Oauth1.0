using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Concrete.Domain
{
    public class Order
    {
        public long order_id { get; set; }
        public int Code { get; set; }
        public long receipt_id { get; set; }
        public int receipt_type { get; set; }

        public int seller_user_id { get; set; }
        public int buyer_user_id { get; set; }
        public string creation_tsz { get; set; }
        public bool can_refund { get; set; }
        public string last_modified_tsz { get; set; }
        public string name { get; set; }
        public string first_line { get; set; }
        public string second_line { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string formatted_address { get; set; }
        public string payment_method { get; set; }
        public string payment_email { get; set; }
        public string message_from_seller { get; set; }
        public string message_from_buyer { get; set; }
        public bool was_paid { get; set; }
        public string total_tax_cost { get; set; }
        public string total_vat_cost { get; set; }
        public string total_price { get; set; }
        public string total_shipping_cost { get; set; }
        public string currency_code { get; set; }
        public string message_from_payment { get; set; }
        public bool was_shipped { get; set; }
        public string buyer_email { get; set; }
        public string seller_email { get; set; }
        public bool is_gift { get; set; }
        public bool needs_gift_wrap { get; set; }
        public string gift_message { get; set; }
        public string discount_amt { get; set; }
        public string etsy_coupon_discount_amt { get; set; }
        public string subtotal { get; set; }
        public string grandtotal { get; set; }
        public string adjusted_grandtotal { get; set; }
        public string buyer_adjusted_grandtotal { get; set; }
        public string shipped_date { get; set; }
        public bool is_overdue { get; set; }
        public string days_from_due_date { get; set; }
        public string transparent_price_message { get; set; }
        public bool show_channel_badge { get; set; }
        public string channel_badge_suffix_string { get; set; }
        public bool is_dead { get; set; }
        


    }

}
