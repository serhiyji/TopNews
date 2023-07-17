using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Services
{
    public class ServiceResponse
    {
        public ServiceResponse() { }
        public ServiceResponse(bool success)
        {
            this.Success = success;
        }
        public ServiceResponse(bool success, string message) : this(success)
        {
            this.Message = message;
        }
        public ServiceResponse(bool success, string message, object payload) : this(success, message)
        {
            this.Payload = payload;
        }
        public ServiceResponse(bool success, string message, IEnumerable<object> errors) : this(success, message)
        {
            this.Errors = errors;
        }
        public ServiceResponse(bool success, string message, object payload, IEnumerable<object> errors) : this(success, message, payload)
        {
            this.Errors = errors;
        }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public object Payload { get; set; } = null;
        public IEnumerable<object> Errors { get; set; } = Enumerable.Empty<object>();
    }
}
