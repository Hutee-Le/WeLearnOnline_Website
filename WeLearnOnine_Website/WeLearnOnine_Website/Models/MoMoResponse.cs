namespace WeLearnOnine_Website.Models
{
    public class MoMoResponse
    {
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public long amount { get; set; }
        public string partnerUserId { get; set; }
        public string orderInfo { get; set; }
        public string orderType { get; set; }
        public long transId { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; }
        public int resultCode { get; set; }
        public string payType { get; set; }
        public string payUrl { get; set; }
        public string extraData { get; set; }
        public string shortLink { get; set; }
        public string signature { get; set; }
    }
}
