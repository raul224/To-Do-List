using System;
using System.Collections.Generic;
using System.Net;
using Infra.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzFunGetById
{
    public static class GetById
    {
        [Function("GetById")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var repositorio = new TodoItemRepositorioCosmos();

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var id = Guid.Parse(query["id"]);

            return new OkObjectResult(repositorio.GetById(id));
        }
    }
}
