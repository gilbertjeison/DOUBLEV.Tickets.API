using BusinessRules.Interfaces;
using DOUBLEV.Tickets.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Utilities.CustomModels;
using Utilities.Enumerations;

namespace DOUBLEV.Tickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : BaseController<Entities.Entities.Ticket, BusinessRules.Interfaces.ITicket>
    {
        public TicketController(ITicket repoBusinessRules) : base(repoBusinessRules)
        {
        }

        #region Lists
        [HttpGet("GetPaged/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPaged(int page) { return GetListOrderPaged(page, Constants.DefaultNumeroDeRegistros, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString()); }

        [HttpGet("GetPaged/{page}/{numberRecords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPaged(int page, int numberRecords) { return GetListOrderPaged(page, numberRecords, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString()); }

        [HttpGet("GetRecordsOrderPaged/{page}/{numberRecords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetRecordsOrderPaged(int page, int numberRecords) { return GetListOrderPaged(page, numberRecords, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString()); }

        [HttpGet("GetRecordsOrderPaged/{page}/{numberRecords}/{orderBy}/{direcOrder}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetRecordsOrderPaged(int page, int numberRecords, string orderBy, string direcOrder) { return GetListOrderPaged(page, numberRecords, orderBy, direcOrder); }

        [HttpGet("GetOrderPaged/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetOrderPaged(int page) { return GetListOrderPaged(page, Constants.DefaultNumeroDeRegistros, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString()); }

        [HttpGet("GetOrderPaged/{page}/{orderBy}/{direcOrder}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetOrderPaged(int page, string orderBy, string direcOrder) { return GetListOrderPaged(page, Constants.DefaultNumeroDeRegistros, orderBy, direcOrder); }

        #endregion

        #region Filtered Lists

        [HttpPost("GetPaged/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPagedFiltering(int page, [FromBody] Filter objectFilter)
        {
            return GetListOrderPaged(page, Constants.DefaultNumeroDeRegistros, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString(), objectFilter);
        }

        [HttpPost("GetPaged/{page}/{numberRecords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPagedFiltering(int page, int numberRecords, [FromBody] Filter objectFilter)
        {
            return GetListOrderPaged(page, numberRecords, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString(), objectFilter);
        }

        [HttpPost("GetRecordsOrderPaged/{page}/{numberRecords}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetRecordsOrderPagedFiltering(int page, int numberRecords, [FromBody] Filter objectFilter)
        {
            return GetListOrderPaged(page, numberRecords, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString(), objectFilter);
        }

        [HttpPost("GetRecordsOrderPaged/{page}/{numberRecords}/{orderBy}/{direcOrder}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetRecordsOrderPagedFiltering(int page, int numberRecords, string orderBy, string direcOrder, [FromBody] Filter objectFilter)
        {
            return GetListOrderPaged(page, numberRecords, orderBy, direcOrder, objectFilter);
        }

        [HttpPost("GetOrderPaged/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetOrderPagedFiltering(int page, [FromBody] Filter objectFilter)
        {
            return GetListOrderPaged(page, Constants.DefaultNumeroDeRegistros, nameof(Entities.Entities.Ticket.UserName), EnumerationApplication.Orden.Asc.ToString(), objectFilter);
        }

        [HttpPost("GetOrderPaged/{page}/{orderBy}/{direcOrder}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomList<Entities.Entities.Ticket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetOrderPagedFiltering(int page, string orderBy, string direcOrder, [FromBody] Filter objectFilter)
        {
            return GetListOrderPaged(page, Constants.DefaultNumeroDeRegistros, orderBy, direcOrder, objectFilter);
        }

        #endregion

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Entities.Entities.Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> Get(long id)
        {
            return GetGenericAsync(x => x.Id == id, id.ToString());
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> Delete(long id)
        {
            return DeleteGenericAsync(x => x.Id == id, id.ToString());
        }
    }
}
