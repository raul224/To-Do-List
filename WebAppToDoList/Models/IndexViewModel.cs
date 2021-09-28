using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppToDoList.Models
{
    public class IndexViewModel
    {
        public TodoModel form { get; set; }
        public List<TodoModel> listaTodos { get; set; } = new List<TodoModel>();
    }
}
