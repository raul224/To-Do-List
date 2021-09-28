using System.IO;
using System.Threading.Tasks;
using Infra.Model;
using Infra.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzFunCriaToDo
{
    public static class CriaToDo
    {
        [Function("CriaToDo")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<itemRequest>(requestBody);

            if (data == null)
            {
                return new BadRequestObjectResult(new { message = "Dados para criar uma tarefa são obrigatórios"});
            }

            var repositorio = new TodoItemRepositorioCosmos();

            await repositorio.Save(data);

            return new CreatedResult("", data);
        }
    }
}
