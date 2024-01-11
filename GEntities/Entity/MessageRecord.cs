using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LarEntities.Entity
{
    public class MessageRecord
    {
        public HttpStatusCode Type { get; set; }
        public string? Detail { get; set; }
    }
}
