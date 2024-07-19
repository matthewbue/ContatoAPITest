using Contatos.Application.Dtos;
using Contatos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Application.Interface
{
    public interface IContatoService
    {
        string CreateContato(ContatoRequestDto entrada);
        string UpdateContato(ContatoUpdateDto entrada);
        List<ContatoResponseDto> GetAll();
        string Delete(int Id);
        string DesativarContato(int Id);
        ContatoResponseDto GetById(int Id);
    }
}
