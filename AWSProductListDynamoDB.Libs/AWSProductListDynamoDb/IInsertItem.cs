using System.Threading.Tasks;

namespace AWSProductListDynamoDb.Libs.AWSProductListDynamoDb
{
    public interface IInsertItem
    {
        Task AddNewEntry(string productName, int productQuantity);
    }
}
