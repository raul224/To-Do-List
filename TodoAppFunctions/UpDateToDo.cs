using System;
using System.IO;
using System.Threading.Tasks;
using Infra.Model;
using Infra.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace AzFunUpdateToDo
{
    public static class UpDateToDo
    {
        [Function("UpdateTodo")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequestData req,
        FunctionContext executionContext)
        {
            var repositorio = new TodoItemRepositorioCosmos();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<itemRequest>(requestBody);

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var id = Guid.Parse(query["id"]);

            if (data == null)
            {
                return new BadRequestObjectResult(new { message = "Dados para criar uma tarefa são obrigatórios" });
            }

            await repositorio.UpdateToDo(id, data);

            return new CreatedResult("", data);
        }
    }
}
