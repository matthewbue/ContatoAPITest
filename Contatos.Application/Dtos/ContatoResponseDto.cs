using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Application.Dtos
{
    public class ContatoResponseDto
    {
        public int Id { get; set; }
        public string NomeContato { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }
    }
}
