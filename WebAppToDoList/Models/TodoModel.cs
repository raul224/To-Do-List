using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppToDoList.Models
{
    public class TodoModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Responsavel { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}
