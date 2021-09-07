using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentalApp.Models
{
    [MetadataType(typeof(customerMetaDate))]
    public partial class Customer
    {
        public class customerMetaDate
        {
            [DisplayName("CustomerName")]
            public string custname { get; set; }
            [DisplayName("Address")]
            public string address { get; set; }
            [DisplayName("Mobile")]
            public Nullable<int> mobile { get; set; }
        }
    }
}