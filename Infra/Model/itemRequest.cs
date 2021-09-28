using Newtonsoft.Json;

namespace Infra.Model
{
    public class itemRequest
    {
        public string responsavel { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
    }

}