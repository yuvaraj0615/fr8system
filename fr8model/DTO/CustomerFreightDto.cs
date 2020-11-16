using System;
using System.Collections.Generic;
using System.Text;

namespace fr8model.DTO
{
    public class CustomerFreightDto
    {
        public string CustomerName { get; set; }
        public string CarrierName { get; set; }
        public int LoadWeight { get; set; }
        public string FromState { get; set; }
        public string ToState { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal TaxPersent { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
