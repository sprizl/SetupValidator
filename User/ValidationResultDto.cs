using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class ValidationResultDto<T>
    {
        public T Source { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
