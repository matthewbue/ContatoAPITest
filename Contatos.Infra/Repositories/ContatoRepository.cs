using Contatos.Domain.Entities;
using Contatos.Domain.Interface;
using Contatos.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Infra.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly ApplicationDbContext _context;

        public ContatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Contato entrada)
        {
            _context.Add(entrada);
            _context.SaveChanges();
        }

        public void Delete(Contato Id)
        {
            _context.Remove(Id);
            _context.SaveChanges();
        }

        public  List<Contato> GetAll()
        {
            var result =  _context.Contato.ToList();
            return result;
        }

        public Contato GetById(int Id)
        {
            var result = _context.Contato.Where(c => c.Id == Id).FirstOrDefault();
            return result;
        }

        public void Update(Contato entrada)
        {
            _context.Update(entrada);
            _context.SaveChanges();
        }
    }
}
