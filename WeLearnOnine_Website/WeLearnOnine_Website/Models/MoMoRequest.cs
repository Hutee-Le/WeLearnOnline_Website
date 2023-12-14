﻿namespace WeLearnOnine_Website.Models
{
    public class MoMoRequest
    {
        public string orderInfo { get; set; }
        public string partnerCode { get; set; }
        public string redirectUrl { get; set; }
        public string ipnUrl { get; set; }
        public long amount { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public string extraData { get; set; }
        public string partnerName { get; set; }
        public string storeId { get; set; }
        public string requestType { get; set; }
        public int orderExpireTime { get; set; }
        public bool autoCapture { get; set; }
        public string lang { get; set; }
        public string signature { get; set; }
    }
}
