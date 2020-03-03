using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupValidator.DTOs
{
    public class ValidationResultDto<T>
    {
        public T Source { get; set; }
        public bool IsPass { get; set; }
        public string Message { get; set; }
    }
}