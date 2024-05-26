using Auth.Models.Membership;
using Auth.Models.User;
using Microsoft.Azure.Cosmos;
using System.Data.SqlClient;
using System.Net;

namespace Auth.Repositories
{
    public class MembershipRepository : IRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _databaseId;
        private readonly string _containerId;

        public MembershipRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["CosmosDb:ConnectionString"];
            _databaseId = _configuration["CosmosDb:DatabaseId"];
            _containerId = _configuration["CosmosDb:ContainerId"];
        }

        public async Task<UserDTO> AddNewUser(UserDTO user)
        {
            try
            {
                using (var client = new CosmosClient(_connectionString))
                {
                    var container = client.GetDatabase(_databaseId).GetContainer(_containerId);
                    ItemResponse<UserDTO> response = await container.CreateItemAsync(user, new PartitionKey(user.Id));

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        return response.Resource; // Return the added user
                    }
                    else
                    {
                        throw new Exception($"Error adding user: {response.StatusCode}"); // Handle creation errors
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Re-throw the exception for handling in the calling method
            }
        }

        public async Task<UserDTO> GetUserByPhone(string phone)
        {
            using (var client = new CosmosClient(_connectionString))
            {
                var container = client.GetDatabase(_databaseId).GetContainer(_containerId);
                string queryByPhone = $"SELECT * FROM users WHERE users.phone = @phone";

                var iterator = container.GetItemQueryIterator<UserDTO>(queryByPhone);

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    foreach (var user in response)
                    {
                        return user; // Return the first user found (assuming unique phone)
                    }
                }

                return null; // No user found with the specified phone
            }
        }

    }
}
