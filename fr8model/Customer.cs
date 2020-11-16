using System;

namespace fr8model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CarrierName { get; set; }
        public int LoadWeight { get; set; }
        public string FromState { get; set; }
        public string ToState { get; set; }
        public int ChargeAmount { get; set; }
    }
}
