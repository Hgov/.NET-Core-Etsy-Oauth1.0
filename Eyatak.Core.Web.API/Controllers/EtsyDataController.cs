using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Eyatak.Core.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EtsyDataController : ControllerBase
    {
        private const string RequestUrl = "https://openapi.etsy.com/v3/public/oauth/token";
        private const string RequestAccessTokenUrl = "https://openapi.etsy.com/v3/public/oauth/token";

        private const string ConsumerKey = "o5hbz2qom64q641n66pxntg9";
        private const string ConsumerSecret = "phifk8t5xu";
        private const string shopname = "PaylessHome";
        private const string shopID = "19025685";
        private static string OAuthToken { get; set; }
        private static string OAuthTokenSecret { get; set; }
        private static string TokenSecret { get; set; }


        public EtsyDataController()
        {
        }
        [HttpGet]
        public IActionResult Login()
        {

            // Configure our OAuth client
            var client = new OAuthRequest
            {
                
                Method = "GET",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                RequestUrl = RequestUrl,
                CallbackUrl = "https://localhost:44351/api/etsydata/callback",
                
            };
            
            // Build request url and send the request
            var url = client.RequestUrl + "?" + client.GetAuthorizationQuery();
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            // Parse login_url and oauth_token_secret from response
            var loginUrl = HttpUtility.ParseQueryString(responseFromServer).Get("login_url");
            TokenSecret = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token_secret");

            return Redirect(loginUrl);
        }

        [HttpGet]
        public IActionResult Callback()
        {
            // Read token and verifier
            string token = Request.Query["oauth_token"];
            string verifier = Request.Query["oauth_verifier"];

            // Create access token request
            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = token,
                TokenSecret = TokenSecret,
                RequestUrl = RequestAccessTokenUrl,
                Verifier = verifier
            };

            // Build request url and send the request
            var url = client.RequestUrl + "?" + client.GetAuthorizationQuery();
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            // Parse and save access token and secret
            OAuthToken = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token");
            OAuthTokenSecret = HttpUtility.ParseQueryString(responseFromServer).Get("oauth_token_secret");

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOpenOrders()
        {
            const string requestUrl = "https://openapi.etsy.com/v2/shops/" + shopID + "/receipts/all?";

            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = OAuthToken,
                TokenSecret = OAuthTokenSecret,
                RequestUrl = requestUrl,
            };

            var url = requestUrl + client.GetAuthorizationQuery();

            var result = await url.GetStringAsync();


            #region open orders json to list
            //var order = JsonConvert.DeserializeObject<MainOrder>(result.ToString());
            //foreach (var abstractOrder in order.results)
            //{
            //    var isdata = uow.orderRepository.isdata((int)abstractOrder.order_id);
            //    #region database
            //    if (!isdata)
            //    {

            //        var creation_tsz = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(abstractOrder.creation_tsz));
            //        abstractOrder.creation_tsz = creation_tsz.ToString();
            //        var last_modified_tsz = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(abstractOrder.last_modified_tsz));
            //        abstractOrder.last_modified_tsz = last_modified_tsz.ToString();
            //        var shipped_date = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(abstractOrder.shipped_date));
            //        abstractOrder.shipped_date = shipped_date.ToString();
            //        var days_from_due_date = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(abstractOrder.days_from_due_date));
            //        abstractOrder.days_from_due_date = days_from_due_date.ToString();
            //        if (abstractOrder.gift_message == "" || abstractOrder.second_line == null) abstractOrder.gift_message = "No Gift Message";
            //        if (abstractOrder.second_line == "" || abstractOrder.second_line == null) abstractOrder.second_line = " ";
            //        if (abstractOrder.state == "" || abstractOrder.state == null) abstractOrder.state = " ";
            //        uow.orderRepository.Add(
            //              new Order
            //              {
            //                  receipt_id = abstractOrder.receipt_id,
            //                  receipt_type = abstractOrder.receipt_type,
            //                  order_id = abstractOrder.order_id,
            //                  seller_user_id = abstractOrder.seller_user_id,
            //                  buyer_user_id = abstractOrder.buyer_user_id,
            //                  creation_tsz = abstractOrder.creation_tsz,
            //                  can_refund = abstractOrder.can_refund,
            //                  last_modified_tsz = abstractOrder.last_modified_tsz,
            //                  name = abstractOrder.name,
            //                  first_line = abstractOrder.first_line,
            //                  second_line = abstractOrder.second_line,
            //                  city = abstractOrder.city,
            //                  state = abstractOrder.state,
            //                  zip = abstractOrder.zip,
            //                  formatted_address = abstractOrder.formatted_address,
            //                  country_id = abstractOrder.country_id,
            //                  payment_method = abstractOrder.payment_method,
            //                  payment_email = abstractOrder.payment_email,
            //                  message_from_seller = abstractOrder.message_from_seller,
            //                  message_from_buyer = abstractOrder.message_from_buyer,
            //                  was_paid = abstractOrder.was_paid,
            //                  total_tax_cost = abstractOrder.total_tax_cost,
            //                  total_vat_cost = abstractOrder.total_vat_cost,
            //                  total_price = abstractOrder.total_price,
            //                  total_shipping_cost = abstractOrder.total_shipping_cost,
            //                  currency_code = abstractOrder.currency_code,
            //                  message_from_payment = abstractOrder.message_from_payment,
            //                  was_shipped = abstractOrder.was_shipped,
            //                  buyer_email = abstractOrder.buyer_email,
            //                  seller_email = abstractOrder.seller_email,
            //                  is_gift = abstractOrder.is_gift,
            //                  needs_gift_wrap = abstractOrder.needs_gift_wrap,
            //                  gift_message = abstractOrder.gift_message,
            //                  discount_amt = abstractOrder.discount_amt,
            //                  etsy_coupon_discount_amt = abstractOrder.etsy_coupon_discount_amt,
            //                  subtotal = abstractOrder.subtotal,
            //                  grandtotal = abstractOrder.grandtotal,
            //                  adjusted_grandtotal = abstractOrder.adjusted_grandtotal,
            //                  buyer_adjusted_grandtotal = abstractOrder.buyer_adjusted_grandtotal,
            //                  shipped_date = abstractOrder.shipped_date,
            //                  is_overdue = abstractOrder.is_overdue,
            //                  days_from_due_date = abstractOrder.days_from_due_date,
            //                  transparent_price_message = abstractOrder.transparent_price_message,
            //                  show_channel_badge = abstractOrder.show_channel_badge,
            //                  channel_badge_suffix_string = abstractOrder.channel_badge_suffix_string,
            //                  is_dead = abstractOrder.is_dead,
            //                  Status = true,
            //                  Code = 10,
            //                  CreateDate = DateTime.Now


            //              });

            //        ReadReceiptRepo.Add(new ReadReceipt
            //        {
            //            order_id = abstractOrder.order_id,
            //            ReadCount = 0,
            //            Note = ""
            //        });

            //        await GetCountryAll(abstractOrder.country_id);
            //        uow.Complete();
            //        await GetTransactionAll();

            //        #endregion

            //    }

            //}

            #endregion
            return Content(result, "application/json");
        }
        [HttpGet]
        public async Task<ActionResult> GetTransactionAll()
        {
            const string requestUrl = "https://openapi.etsy.com/v2/shops/" + shopID + "/transactions?";

            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = OAuthToken,
                TokenSecret = OAuthTokenSecret,
                RequestUrl = requestUrl,
            };

            var url = requestUrl + client.GetAuthorizationQuery();
            var result = await url.GetStringAsync();
            return Content(result, "application/json");

        }

        [HttpGet("{listing_id}")]
        public async Task<ActionResult> GetImagesByID(long listing_id)
        {
            string requestUrl = "https://openapi.etsy.com/v2/listings/" + listing_id + "/images?";
            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = OAuthToken,
                TokenSecret = OAuthTokenSecret,
                RequestUrl = requestUrl,
            };

            var url = requestUrl + client.GetAuthorizationQuery();
            var result = await url.GetStringAsync();
            return Content(result, "application/json");
        }
        [HttpGet("{country_id}")]
        public async Task<IActionResult> GetCountryByID(int country_id)
        {
            string requestUrl = "https://openapi.etsy.com/v2/countries/" + country_id + "?";

            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = OAuthToken,
                TokenSecret = OAuthTokenSecret,
                RequestUrl = requestUrl,
            };

            var url = requestUrl + client.GetAuthorizationQuery();
            var result = await url.GetStringAsync();
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetShopInfo()
        {
            ///shops/:shop_id/receipts/search
            const string requestUrl = "https://openapi.etsy.com/v2/shops/" + shopID + "?";

            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = OAuthToken,
                TokenSecret = OAuthTokenSecret,
                RequestUrl = requestUrl,
            };

            var url = requestUrl + client.GetAuthorizationQuery();
            var result = await url.GetStringAsync();
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetShopActiveProducts()
        {
            ///shops/:shop_id/receipts/search
            const string requestUrl = "https://openapi.etsy.com/v2/shops/" + shopID + "/listings/active?";

            var client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                Token = OAuthToken,
                TokenSecret = OAuthTokenSecret,
                RequestUrl = requestUrl,
            };

            var url = requestUrl + client.GetAuthorizationQuery();
            var result = await url.GetStringAsync();
            return Content(result, "application/json");
        }
    }
}
