using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Infra.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace AzFunRemoveToDo
{
    public static class RemoveToDo
    {
        [Function("RemoveToDo")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            var repositorio = new TodoItemRepositorioCosmos();

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var id = Guid.Parse(query["id"]);

            var result = repositorio.GetById(id);

            if (result == null)
                return new NotFoundResult();

            await repositorio.Remove(result);

            return new OkResult();
        }
    }
}
