using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppToDoList.Models
{
    public class ResponseModel
    {
        public TodoModel Value { get; set; }
        public IEnumerable<string> Formatters { get; set; }
        public IEnumerable<string> ContentTypes { get; set; }
        public IEnumerable<string> DeclaredType { get; set; }
        public int StatusCode { get; set; }
    }
}
