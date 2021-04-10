using cosmos_sql;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cosmos_trigger
{
    class Program
    {
        static string database = "appdb";
        static string containername = "customer";
        static string endpoint = "https://appaccount4000.documents.azure.com:443/";
        static string accountkeys = "zDVv6P9fb9LOEnmyvr756TFRMcpk2TB3kXAIgWnpIJui1ANnGFFq96QaGhmRKke81AbZqVklzx6jB2fEXIeSIA==";

        static void Main(string[] args)
        {
            CreateItem().Wait();
            Console.ReadLine();
        }

        private static async Task CreateItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                customer obj = new customer(5, "David", "New York");

                ItemResponse<customer> response = await container_conn.CreateItemAsync(obj,null, new ItemRequestOptions { PreTriggers = new List<string> { "AppendDocument" } });
                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Customer added");
            }

            }

        }
}
