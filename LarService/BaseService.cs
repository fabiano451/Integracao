using LarContracts.IService;
using LarEntities.Entity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LarService
{

    public class BaseService : IBaseService
    {
        public List<MessageRecord> MessageList { get; set; }
        private readonly ILogger _logger;

        public BaseService(ILogger<BaseService> logger)
        {
            this.MessageList = new List<MessageRecord>();
            _logger = logger;
        }

        public void AddSystemMessage(HttpStatusCode statusCode) =>
            MessageList.Add(new MessageRecord
            {
                Type = statusCode
            });

        public void AddSystemMessage(HttpStatusCode statusCode, string message) =>
            MessageList.Add(new MessageRecord
            {
                Type = statusCode,
                Detail = message
            });

        public void AddSystemMessage(HttpStatusCode statusCode, Exception exception, string message = "")
        {
            _logger.LogError(exception, message);

            MessageList.Add(new MessageRecord
            {
                Type = statusCode,
                Detail = message
            });
        }

        public bool IsInternalServerError() => MessageList.Any(e => e.Type == HttpStatusCode.InternalServerError);

        public bool IsNotFound() => MessageList.Any(e => e.Type == HttpStatusCode.NotFound);

        public bool IsBadRequest() => MessageList.Any(e => e.Type == HttpStatusCode.BadRequest);

        public bool IsConflict() => MessageList.Any(e => e.Type == HttpStatusCode.Conflict);

        public bool IsNoContent() => MessageList.All(e => e.Type == HttpStatusCode.NoContent);

        public bool IsOK() => MessageList.All(e => e.Type == HttpStatusCode.OK);

        public bool IsForbidden() => MessageList.Any(e => e.Type == HttpStatusCode.Forbidden);

        
    }
}

