using System.Threading.Tasks;
using AWSProductListDynamoDb.Libs.Models;

namespace AWSProductListDynamoDb.Libs.AWSProductListDynamoDb
{
    public interface IUpdateItem
    {
        Task<Item> Update(string productName, int productQuantity);
    }
}