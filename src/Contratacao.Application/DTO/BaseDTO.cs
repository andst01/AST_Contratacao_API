using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Contratacao.Application.DTO
{
    public class BaseDTO
    {

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Mensagem? Mensagem { get; set; }
    }

    public class Mensagem
    {
        public bool Sucesso { get; set; }
        public string Descricao { get; set; }
    }
}
