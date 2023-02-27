using System;
using System.Collections.Generic;
using System.Text;

namespace AWSProductListDynamoDb.Libs.Models
{
  public class DynamoDbTableItems
  {
    public IEnumerable<Item> Items { get; set; }
  }
}
