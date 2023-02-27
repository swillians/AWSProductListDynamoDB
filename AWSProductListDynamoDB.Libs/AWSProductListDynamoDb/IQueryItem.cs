using System.Threading.Tasks;
using AWSProductListDynamoDb.Libs.Models;

namespace AWSProductListDynamoDb.Libs.AWSProductListDynamoDb
{
    public interface IQueryItem
    {
        Task<DynamoDbTableItems> GetItems(string? productName);
    }
}
