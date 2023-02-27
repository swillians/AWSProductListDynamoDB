using System.Threading.Tasks;

namespace AWSProductListDynamoDb.Libs.AWSProductListDynamoDb
{
    public interface IDeleteItem
    {
        Task DeleteEntry(string productName, int productQuantity);
    }
}
