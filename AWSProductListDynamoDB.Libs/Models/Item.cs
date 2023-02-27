using System;
using System.Collections.Generic;
using System.Text;

namespace AWSProductListDynamoDb.Libs.Models
{
    public class Item
    {
        public string productName { get; set; }
        public int productQuantity { get; set; }
    }

}
