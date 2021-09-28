using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infra.Model
{
    public class TodoItem
    {

        public Guid id { get; set; }
        public string responsavel { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public estado estado { get; set; }
        [JsonProperty(PropertyName = "pk")]
        public string pk = "todo";
    }
}
