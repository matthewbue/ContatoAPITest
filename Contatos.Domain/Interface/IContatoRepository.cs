using Contatos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Domain.Interface
{
    public interface IContatoRepository
    {
        public void Create(Contato entrada);
        public void Update(Contato entrada);
        public void Delete(Contato Id);
        public Contato GetById(int Id);
        public List<Contato> GetAll();
    }
}
