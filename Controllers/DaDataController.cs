using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DaData.Models;

#region DaDataController
namespace DaData.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DaDataController : ControllerBase
    {
        private readonly DaDataContext _context;
        #endregion

        public DaDataController(DaDataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получение списка всех городов
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/dadata HTTP/1.1
        ///     Host: localhost:5000
        ///     Content-Type: application/json
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3pkIiwibmJmIjoiMTUyODAzOTExOSIsImV4cCI6IjE1MjgxMjU1MTkifQ.6TFDPCafEv4IjYFyUoWd3BDzEfd-zvZPPUh5Xa1mXgk
        ///     Cache-Control: no-cache
        ///     Postman-Token: e6d42ee4-45cb-4bfb-97b7-26bbdcf05c49
        /// </remarks>
        /// <returns>Список городов</returns>
        /// <response code="401">Unauthorized</response>
        #region snippet_Get
        [HttpGet]
        public List<city> GetAll()
        {
            return _context.as_addrobj.ToList();
        }
        /// <summary>
        /// Получение списка город начинающихся с name
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/dadata/%D0%A3%D1%84%D0%B0 HTTP/1.1
        ///     Host: localhost:5000
        ///     Content-Type: application/json
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3pkIiwibmJmIjoiMTUyODAzOTExOSIsImV4cCI6IjE1MjgxMjU1MTkifQ.6TFDPCafEv4IjYFyUoWd3BDzEfd-zvZPPUh5Xa1mXgk
        ///     Cache-Control: no-cache
        ///     Postman-Token: d2209824-da7a-4667-86a4-17193d0d7a34
        /// </remarks>
        /// <returns>Список городов</returns>
        /// <response code="401">Unauthorized</response>        
        /// <response code="404">NotFound</response>
        #region snippet_GetByName
        [HttpGet("{name}", Name = "GetTodo")]
        public IActionResult GetByName(string name)
        {
            var item = _context.as_addrobj.Where(x => x.off_name.StartsWith(name)).ToList();
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        #endregion
        #endregion
    }
}