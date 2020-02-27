using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupValidator.Domain
{
    public class ValidationResult
    {
        public bool IsPass { get; private set; }
        public string Message { get; private set; }

        public ValidationResult(bool pass, string message = "")
        {
            IsPass = pass;
            Message = message;
        }
    }
}