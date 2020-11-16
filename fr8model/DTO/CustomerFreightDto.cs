using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Currency)]
        public decimal ChargeAmount { get; set; }

        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        [Display(Name = "Tax")]
        public decimal TaxPersent { get; set; }

        public string TotalAmount { get; set; }
    }
}
