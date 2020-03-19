using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Magni.APIClient.V2.Models
{
    public class APIResponse
    {
        public string RequestId { get; set; }
        public string Type { get; set; }
        public ErrorType ErrorValue { get; set; }
        public string ErrorHumeReadable { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }

        public APIResponse()
        {
        }

        internal APIResponse(Invoicing_v2.Response response)
        {
            this.Type = response.Type.ToString();
            this.RequestId = response.RequestId;
            this.ErrorHumeReadable = response.ErrorHumanReadable;

            if (response.ErrorValue != null)
            {
                this.ErrorValue = new ErrorType()
                {
                    Name = response.ErrorValue.Name,
                    Value = response.ErrorValue.Value

                };
            }

            if (response.ValidationErrors != null && response.ValidationErrors.Any())
            {
                this.ValidationErrors = response.ValidationErrors.Select(error => new ValidationError()
                {
                    Detail = error.Detail,
                    ElementNumber = error.ElementNumber,
                    Field = error.Field,
                    FieldName = error.FieldName,
                    Type = error.Type
                })
                .ToList();
            }
        }
    }

    public class ErrorType
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public class ValidationError
    {
        public string FieldName { get; set; }
        public int ElementNumber { get; set; }
        public string Type { get; set; }
        public string Field { get; set; }
        public string Detail { get; set; }
    }
}