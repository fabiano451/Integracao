using LarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LarContracts.IService
{
   public interface IBaseService
    {
        List<MessageRecord> MessageList { get; set; }

        public void AddSystemMessage(HttpStatusCode statusCode);

        public void AddSystemMessage(HttpStatusCode statusCode, string message);

        public void AddSystemMessage(HttpStatusCode statusCode, Exception exception, string message = "");

        public bool IsInternalServerError();

        public bool IsNotFound();

        public bool IsBadRequest();

        public bool IsConflict();

        public bool IsNoContent();

        public bool IsForbidden();

        public bool IsOK();
    }
}
