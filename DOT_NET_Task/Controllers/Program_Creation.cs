using Dot_NET_Task.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Data.Common;

namespace Dot_NET_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Program_Creation : ControllerBase
    {
        private readonly string CosmosDBAccountUri = "https://localhost:8081/";
        private readonly string CosmosDBAccountPrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string CosmosDbName = "Dotnet_Test";
        private readonly string CosmosDbContainerName = "Program";

        private Container ContainerClient()
        {
            CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication(ProgramModel Program)
        {
            try
            {
                var container = ContainerClient();
                var response = await container.CreateItemAsync(Program, new PartitionKey(Program.ProgramTitle));
                return Ok("Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationDetails()
        {
            try
            {
                var container = ContainerClient();
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<ProgramModel> queryResultSetIterator = container.GetItemQueryIterator<ProgramModel>(queryDefinition);
                List<ProgramModel> Programs = new List<ProgramModel>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<ProgramModel> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (ProgramModel Program in currentResultSet)
                    {
                        Programs.Add(Program);
                    }
                }
                return Ok(Programs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(ProgramModel pro, string partitionKey)
        {
            try
            {
                var container = ContainerClient();
                ItemResponse<ProgramModel> res = await container.ReadItemAsync<ProgramModel>(pro.id, new PartitionKey(partitionKey));
                
                var existingItem = res.Resource;
                existingItem.ProgramTitle = pro.ProgramTitle;
                existingItem.ProgramDescription = pro.ProgramDescription;
                existingItem.PersonalInformations = pro.PersonalInformations;
                existingItem.Questions = pro.Questions;
                var updateRes = await container.ReplaceItemAsync(existingItem, pro.id, new PartitionKey(partitionKey));
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApplication(string proId, string partitionKey)
        {
            try
            {
                var container = ContainerClient();
                var response = await container.DeleteItemAsync<ProgramModel>(proId, new PartitionKey(partitionKey));
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
