using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSProductListDynamoDb.Libs.AWSProductListDynamoDb
{
    public class InsertItem : IInsertItem
    {
        private static readonly string tableName = Environment.GetEnvironmentVariable("AWS_CONTENT");

        private readonly IAmazonDynamoDB _dynamoDbClient;
        public InsertItem(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }
        public async Task AddNewEntry(string productName, int productQuantity)
        {
            try
            {
                var queryRequest = RequestBuilder(productName, productQuantity);

                await PutItemAsync(queryRequest);
            }
            catch (InternalServerErrorException)
            {
                Console.WriteLine("Erro 1");
            }
            catch (ResourceNotFoundException)
            {
                Console.WriteLine("Erro 2");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.StackTrace.ToString());
            }

        }

        private PutItemRequest RequestBuilder(string productName, int productQuantity)
        {
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();

            attributes["ProductName"] = new AttributeValue { S = productName.ToString() };
            attributes["ProductQuantity"] = new AttributeValue { N = productQuantity.ToString() };

            return new PutItemRequest
            {
                TableName = tableName,
                Item = attributes
            };
        }
        private async Task PutItemAsync(PutItemRequest request)
        {
            try
            {
                await _dynamoDbClient.PutItemAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.InnerException.StackTrace.ToString());
            }
        }
    }
}
