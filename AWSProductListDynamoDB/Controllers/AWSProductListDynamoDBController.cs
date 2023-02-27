using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWSProductListDynamoDb.Libs.AWSProductListDynamoDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWSProductListDynamoDb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    public class AWSProductListDynamoDBController : Controller //Base
    {
        private readonly IAWSProductListDynamoDbExamples _dynamoDbExamples;
        private readonly IInsertItem _insertItem;
        private readonly IQueryItem _queryItem;
        private readonly IDeleteItem _deleteItem;
        private readonly IUpdateItem _updateItem;

        public AWSProductListDynamoDBController(IAWSProductListDynamoDbExamples dynamoDbExamples, IInsertItem insertItem, IQueryItem queryItem, IDeleteItem deleteItem, IUpdateItem updateItem)
        {
            _dynamoDbExamples = dynamoDbExamples;
            _insertItem = insertItem;
            _queryItem  = queryItem;
            _deleteItem = deleteItem;
            _updateItem = updateItem;
        }

        [Route("createtable")]
        public IActionResult CreateDynamoDbTable()
        {
            _dynamoDbExamples.CreateDynamoDbTable();

            return Ok();
        }

        [HttpPost]
        [Route("insertitem")]
        public IActionResult InsertItem([FromQuery] string productName, int productQuantity)
        {
            _insertItem.AddNewEntry(productName, productQuantity);

            return Ok();
        }
        [HttpGet]
        [Route("queryitems")]
        public async Task<IActionResult> GetItems([FromQuery] string productName)
        {
            var response = await _queryItem.GetItems(productName);

            return Ok(response);
        }
        [HttpDelete]
        [Route("deleteitems")]
        public IActionResult DeleteItems([FromQuery] string productName, int productQuantity)
        {
            _deleteItem.DeleteEntry(productName,productQuantity);

            return Ok();
        }
        [HttpPut]
        [Route("updateitem")]
        public async Task<IActionResult> UpdateItem([FromQuery] string productName, int productQuantity)
        {
            var response = await _updateItem.Update(productName, productQuantity);

            return Ok(response);
        }
    }
}