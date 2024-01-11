using LarContracts.IService;
using LarEntities.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace WepApi.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;
        public List<MessageRecord> MessageList { get; set; }

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
            this.MessageList = new List<MessageRecord>();
        }

        private void AddSystemMessage(HttpStatusCode statusCode, string message) =>
            MessageList.Add(new MessageRecord
            {
                Type = statusCode,
                Detail = message
            });

        protected InternalServerErrorObjectResult InternalServerError()
        {
            return new InternalServerErrorObjectResult();
        }

        protected InternalServerErrorObjectResult InternalServerError(string message)
        {
            var problemDetail = Problem(detail: message);

            return new InternalServerErrorObjectResult(problemDetail.Value);
        }

        protected InternalServerErrorObjectResult InternalServerError(object value)
        {
            return new InternalServerErrorObjectResult(value);
        }

        protected IActionResult TreatOkResult(Object response, IBaseService baseService)
        {
            return response != null ? Ok(response) : TreatResult(baseService);
        }

        protected IActionResult TreatCreatedResult(string uri, Object response, IBaseService baseService)
        {
            return response != null ? Created(uri, response) : TreatResult(baseService);
        }

        protected void AddValidationError(string error)
        {
            AddSystemMessage(HttpStatusCode.BadRequest, error);
        }

        protected void AddValidationError(List<ValidationFailure> validationFailures)
        {
            //foreach (var item in validationFailures)
            //    AddSystemMessage(HttpStatusCode.BadRequest, item.ErrorMessage);
        }

        protected bool HasValidationError()
         => MessageList.Any(m => m.Type == HttpStatusCode.BadRequest);

        protected IActionResult ValidationErrors()
        {
            return BadRequest(MessageList.Where(m => m.Type == HttpStatusCode.BadRequest));
        }

        protected IActionResult TreatResult(IBaseService baseService)
        {
            if (baseService.IsNotFound())
            {
                return NotFound(baseService.MessageList.Where(m => m.Type == HttpStatusCode.NotFound));
            }
            else if (baseService.IsBadRequest())
            {
                return BadRequest(baseService.MessageList.Where(m => m.Type == HttpStatusCode.BadRequest));
            }
            else if (baseService.IsConflict())
            {
                return Conflict(baseService.MessageList.Where(m => m.Type == HttpStatusCode.Conflict));
            }
            else if (baseService.IsNoContent())
            {
                return NoContent();
            }
            else if (baseService.IsForbidden())
            {
                return Forbid();
            }
            else
            {
                return InternalServerError(baseService.MessageList.Where(m => m.Type == HttpStatusCode.InternalServerError));
            }
        }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorObjectResult() : this(null)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}

