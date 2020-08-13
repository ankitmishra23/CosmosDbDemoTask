using CosmosDemo.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDemo.Services
{
    public class Emp:IEmp
    {
        private Microsoft.Azure.Cosmos.Container container;
        public Emp(CosmosClient cosmosClient, string databaseName, string collectionName)
        {
            this.container = cosmosClient.GetContainer(databaseName, collectionName);
        }
        public Task AddEmployeeAsync(Employee employee)
        {
            return this.container.CreateItemAsync<Employee>(employee, new PartitionKey(employee.Id));
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            await this.container.DeleteItemAsync<Employee>(id, new PartitionKey(id));
        }

        public async Task<Employee> GetEmployeeAsync(string id)
        {
            try
            {
                ItemResponse<Employee> itemResponse = await this.container.ReadItemAsync<Employee>(id, new PartitionKey(id));
                return itemResponse.Resource;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string query)
        {
            var querys = this.container.GetItemQueryIterator<Employee>(new QueryDefinition(query));
            List<Employee> emp = new List<Employee>();
            while (querys.HasMoreResults)
            {
                var reponse = await querys.ReadNextAsync();
                emp.AddRange(reponse.ToList());
            }
            return emp;
        }

        public async Task UpdateEmployeeAsync(string id, Employee employee)
        {
            await this.container.UpsertItemAsync<Employee>(employee, new PartitionKey(id));
        }
    }
}
