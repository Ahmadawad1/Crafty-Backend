using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace CentralLoggingService
{
    public class CentralLoggingService
    {

        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseId;
        private readonly string _containerId;

        public CentralLoggingService(IConfiguration configuration)
        {
            _databaseId = "";
            _containerId = "";
        }

        public async void Log(LogRecord logRecord)
        {
            try
            {
                Database database = _cosmosClient.GetDatabase(_databaseId);
                Container container = database.GetContainer(_containerId);
                ItemResponse<LogRecord> response = await container.CreateItemAsync(logRecord);
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }

}

