using Infra.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppToDoList.Models;

namespace WebAppToDoList.Controllers
{
    public class ToDosController : Controller
    {
        private string URL = "https://todoappfunctionsat.azurewebsites.net/api/";

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            var request = await httpClient.GetAsync($"{URL}GetTodosToDo");
            var response = await request.Content.ReadAsStringAsync();
            ResponseModelList todos = JsonConvert.DeserializeObject<ResponseModelList>(response);

            foreach(var item in todos.Value)
            {
                switch (item.Estado)
                {
                    case "2":
                        item.Estado = "Fazendo";
                        break;
                    case "3":
                        item.Estado = "Feito";
                        break;
                    default:
                        item.Estado = "Backlog";
                        break;
                }
            }

            return View(todos.Value);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var request = await httpClient.GetAsync($"{URL}GetById?id={id}");
            var response = await request.Content.ReadAsStringAsync();
            ResponseModel todo = JsonConvert.DeserializeObject<ResponseModel>(response);

            switch (todo.Value.Estado)
            {
                case "2":
                    todo.Value.Estado = "Fazendo";
                    break;
                case "3":
                    todo.Value.Estado = "Feito";
                    break;
                default:
                    todo.Value.Estado = "Backlog";
                    break;
            }

            return View(todo.Value);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();

                var todoItem = new itemRequest();
                todoItem.responsavel = collection["Responsavel"];
                todoItem.titulo = collection["Titulo"];
                todoItem.descricao = collection["Descricao"];
                todoItem.estado = collection["Estado"];

                var item = JsonConvert.SerializeObject(todoItem);

                var body = new StringContent(item, System.Text.Encoding.UTF8, "application/json");

                await httpClient.PostAsync($"{URL}CriaToDo", body);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var request = await httpClient.GetAsync($"{URL}GetById?id={id}");
            var response = await request.Content.ReadAsStringAsync();
            ResponseModel todo = JsonConvert.DeserializeObject<ResponseModel>(response);

            return View(todo.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();

                var todoItem = new itemRequest();
                todoItem.responsavel = collection["Responsavel"];
                todoItem.titulo = collection["Titulo"];
                todoItem.descricao = collection["Descricao"];
                todoItem.estado = collection["Estado"];

                var item = JsonConvert.SerializeObject(todoItem);
                var body = new StringContent(item, System.Text.Encoding.UTF8, "application/json");
                await httpClient.PutAsync($"{URL}UpdateTodo?id={id}", body);

                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Edit");
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var request = await httpClient.GetAsync($"{URL}GetById?id={id}");
            var response = await request.Content.ReadAsStringAsync();
            ResponseModel todo = JsonConvert.DeserializeObject<ResponseModel>(response);

            switch (todo.Value.Estado)
            {
                case "2":
                    todo.Value.Estado = "Fazendo";
                    break;
                case "3":
                    todo.Value.Estado = "Feito";
                    break;
                default:
                    todo.Value.Estado = "Backlog";
                    break;
            }

            return View(todo.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            HttpClient httpClient = new HttpClient();

            await httpClient.DeleteAsync($"{URL}RemoveToDo?id={id}");

            return RedirectToAction("Index");
        }
    }
}
