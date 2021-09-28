using Infra.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace AzFunGetTodos
{
    public static class GetTodosToDo
    {
        [Function("GetTodosToDo")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var repositorio = new TodoItemRepositorioCosmos();

            var dados = repositorio.GetAll();

            return new OkObjectResult(repositorio.GetAll());
        }
    }
}
