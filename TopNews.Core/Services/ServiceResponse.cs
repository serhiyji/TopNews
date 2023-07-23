using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Services
{
    public class ServiceResponse<PayloadType, ErrorType>
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
        public ServiceResponse(bool success, string message, PayloadType payload) : this(success, message)
        {
            this.Payload = payload;
        }
        public ServiceResponse(bool success, string message, IEnumerable<ErrorType> errors) : this(success, message)
        {
            this.Errors = errors;
        }
        public ServiceResponse(bool success, string message, PayloadType payload, IEnumerable<ErrorType> errors) : this(success, message, payload)
        {
            this.Errors = errors;
        }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public PayloadType Payload { get; set; } = default;
        public IEnumerable<ErrorType> Errors { get; set; } = Enumerable.Empty<ErrorType>();
    }
    public class ServiceResponse : ServiceResponse<object, object>
    {
        public ServiceResponse() { }

        public ServiceResponse(bool success) : base(success) { }

        public ServiceResponse(bool success, string message) : base(success, message) { }

        public ServiceResponse(bool success, string message, object payload) : base(success, message, payload) { }

        public ServiceResponse(bool success, string message, IEnumerable<object> errors) : base(success, message, errors) { }

        public ServiceResponse(bool success, string message, object payload, IEnumerable<object> errors) : base(success, message, payload, errors) { }
    }
}
