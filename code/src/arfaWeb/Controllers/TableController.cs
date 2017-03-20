using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using arfaWeb.Models.Requests;
using arfaWeb.Models;
using arfa.Interface.Business;
using arfaWeb.Helpers;
using arfaWeb.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace arfaWeb.Controllers
{
    [Route("api/v2/[controller]/[action]")]
    public class TableController : Controller
    {
        private readonly ITableService tableService;

        public TableController(ITableService tableSvc)
        {
            tableService = tableSvc;
        }

        [HttpPost]
        public CreateTableResponse CreateTable([FromBody] CreateTableRequest request)
        {
            try
            {
                var table = tableService.CreateTable(request.loginToken, request.tableName);
                return new CreateTableResponse()
                {
                    table = Table.FromInterface(table),
                    message = "Table created successfully",
                    status = "SUCCESS"
                };
            }
            catch (Exception e)
            {
                return ExceptionHelper.CreateResponse<CreateTableResponse>(e);
            }
        }
    }
}
