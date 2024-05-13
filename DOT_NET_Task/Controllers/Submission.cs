using Dot_NET_Task.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Dot_NET_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Submission : ControllerBase
    {
        private readonly string CosmosDBAccountUri = "https://localhost:8081/";
        private readonly string CosmosDBAccountPrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string CosmosDbName = "Dotnet_Test";
        private readonly string CosmosDbContainerName = "Submission";

        private Container ContainerClient()
        {
            CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuestion(QuestionSubmissionModel submission)
        {
            try
            {
                var container = ContainerClient();
                var response = await container.CreateItemAsync(submission, new PartitionKey(submission.IDNumber));
                return Ok("Submitted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
