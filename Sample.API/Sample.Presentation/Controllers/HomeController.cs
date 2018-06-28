using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Presentation.Models;
using Microsoft.AspNetCore.Cors;

namespace Sample.Presentation.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/jsendpoint/{id}")]
        public async Task<IActionResult> JSEndpoint(int id)
        {
            APIConsumer externalAPI = new APIConsumer();
            string requestedRecord = await externalAPI.ReadAsync(id);
            APIResponseModel externalResponse = new APIResponseModel();
            externalResponse.APIResponse = requestedRecord + "(Passed through JS endpoint controller, rather than direct from JQuery, x1)";

            return View("JSEndpoint", externalResponse);
        }




    }
}
