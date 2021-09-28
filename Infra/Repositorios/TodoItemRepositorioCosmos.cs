using Infra.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class TodoItemRepositorioCosmos
    {
        private string connectionString = "AccountEndpoint=https://todo-cosmos-at-raulpires.documents.azure.com:443/;AccountKey=weYrspCJDwkq2PqWtTKZUGNBry08qIz9qKhpcf0TLcV8edd4iBzRghb552uGguYX7hobhgZfVwO3QAWRuou0hQ==;";
        private string Contanier = "todo-container";
        private string Db = "todo-db";

        private CosmosClient cosmosClient { get; set; }

        public TodoItemRepositorioCosmos()
        {
            this.cosmosClient = new CosmosClient(this.connectionString);
        } 

        public List<TodoItem> GetAll()
        {
            var container = this.cosmosClient.GetContainer(Db, Contanier);
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");
            var queryResult = container.GetItemQueryIterator<TodoItem>(queryDefinition);

            var todosTodo = new List<TodoItem>();
            //percorre a lista por vez, cosmos não traz tudo de uma vez
            while(queryResult.HasMoreResults)
            {
                //resultado de cada varredura
                FeedResponse<TodoItem> resultados = queryResult.ReadNextAsync().Result;

                //aqui pega o range todo de elementos e passa para a lista criada
                todosTodo.AddRange(resultados.Resource);
            }

            return todosTodo;
        }

        public TodoItem GetById(Guid id)
        {
            var container = this.cosmosClient.GetContainer(Db, Contanier);
            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");
            var queryResult = container.GetItemQueryIterator<TodoItem>(queryDefinition);

            TodoItem item = null;

            while (queryResult.HasMoreResults)
            {
                FeedResponse<TodoItem> resultados = queryResult.ReadNextAsync().Result;
                item = resultados.Resource.FirstOrDefault();
            }

            return item;
        }
        public async Task Save(itemRequest criaItem)
        {
            var item = new TodoItem();
            item.id = Guid.NewGuid();
            item.descricao = criaItem.descricao;
            item.titulo = criaItem.titulo;
            item.responsavel = criaItem.responsavel;
            item.estado = Enum.Parse<estado>(criaItem.estado);

            var container = this.cosmosClient.GetContainer(Db, Contanier);
            await container.CreateItemAsync<TodoItem>(item, new PartitionKey(item.pk));
        }
        public async Task UpdateToDo(Guid id, itemRequest criaItem)
        {
            var item = GetById(id);
            item.descricao = criaItem.descricao;
            item.titulo = criaItem.titulo;
            item.responsavel = criaItem.responsavel;
            item.estado = Enum.Parse<estado>(criaItem.estado);

            var container = this.cosmosClient.GetContainer(Db, Contanier);
            await container.ReplaceItemAsync<TodoItem>(item, id.ToString(), new PartitionKey(item.pk));
        }
        public async Task Remove(TodoItem item)
        {
            var container = this.cosmosClient.GetContainer(Db, Contanier);
            await container.DeleteItemAsync<TodoItem>(item.id.ToString(), new PartitionKey(item.pk));
        }
    }
}
