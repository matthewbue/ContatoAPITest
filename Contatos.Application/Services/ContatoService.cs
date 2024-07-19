// Contatos/Application/Services/ContatoService.cs
using Contatos.Application.Dtos;
using Contatos.Application.Interface;
using Contatos.Domain.Entities;
using Contatos.Domain.Interface;
using System;

namespace Contatos.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        private bool ValidateContato(ContatoRequestDto entrada, out string validationMessage)
        {
            validationMessage = string.Empty;
            var idade = DateTime.Now.Year - entrada.DataNascimento.Year;

            if (idade < 18)
            {
                validationMessage = "O contato deve ser maior de idade.";
                return false;
            }

            if (entrada.DataNascimento > DateTime.Now)
            {
                validationMessage = "A data de nascimento não pode ser maior que a data de hoje.";
                return false;
            }

            
            if (DateTime.Now.DayOfYear < entrada.DataNascimento.DayOfYear)
            {
                idade--;
            }


            if (idade == 0)
            {
                validationMessage = "A idade não pode ser igual a 0.";
                return false;
            }

            return true;
        }
        private bool ValidateContatoUpdate(ContatoUpdateDto entrada, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (entrada.DataNascimento > DateTime.Now)
            {
                validationMessage = "A data de nascimento não pode ser maior que a data de hoje.";
                return false;
            }

            var idade = DateTime.Now.Year - entrada.DataNascimento.Year;
            if (DateTime.Now.DayOfYear < entrada.DataNascimento.DayOfYear)
            {
                idade--;
            }

            if (idade == 0)
            {
                validationMessage = "A idade não pode ser igual a 0.";
                return false;
            }

            if (idade < 18)
            {
                validationMessage = "O contato deve ser maior de idade.";
                return false;
            }

            return true;
        }
        public string CreateContato(ContatoRequestDto entrada)
        {
            if (!ValidateContato(entrada, out var validationMessage))
            {
                return validationMessage;
            }

            var objetoentidade = new Contato
            {
                DataNascimento = entrada.DataNascimento,
                NomeContato = entrada.NomeContato,
                Telefone = entrada.Telefone,
                Sexo = entrada.Sexo,
                Ativo = true
            };

            _contatoRepository.Create(objetoentidade);

            return "Sucesso";
        }

        public string UpdateContato(ContatoUpdateDto entrada)
        {
            var objectContato = _contatoRepository.GetById(entrada.Id);
            if (objectContato == null)
            {
                return "Contato não encontrado.";
            }

            if (!ValidateContatoUpdate(entrada, out var validationMessage))
            {
                return validationMessage;
            }

            objectContato.NomeContato = entrada.NomeContato;
            objectContato.Telefone = entrada.Telefone;
            objectContato.Sexo = entrada.Sexo;
            objectContato.DataNascimento = entrada.DataNascimento;

            _contatoRepository.Update(objectContato);

            return "Sucesso";
        }

        public string Delete(int Id)
        {
            var objectContato = _contatoRepository.GetById(Id);
            if (objectContato == null)
            {
                return "Contato não encontrado.";
            }

            _contatoRepository.Delete(objectContato);

            return "Sucesso";
        }

        public string DesativarContato(int Id)
        {
            var objectContato = _contatoRepository.GetById(Id);
            if (objectContato == null)
            {
                return "Contato não encontrado.";
            }

            objectContato.Ativo = false;
            _contatoRepository.Update(objectContato);

            return "Sucesso";
        }

        public List<ContatoResponseDto> GetAll()
        {
            var resultRepositorio = _contatoRepository.GetAll();

            var ListResposta = new List<ContatoResponseDto>();
            foreach (var item in resultRepositorio)
            {
                var objetoResposta = new ContatoResponseDto
                {
                    Idade = DateTime.Now.Year - item.DataNascimento.Year,
                    DataNascimento = item.DataNascimento,
                    NomeContato = item.NomeContato,
                    Telefone = item.Telefone,
                    Sexo = item.Sexo,
                    Id = item.Id,
                    Ativo = item.Ativo
                };

                if (DateTime.Now.DayOfYear < item.DataNascimento.DayOfYear)
                {
                    objetoResposta.Idade--;
                }

                ListResposta.Add(objetoResposta);
            }

            return ListResposta;
        }

        public ContatoResponseDto GetById(int Id)
        {
            var item = _contatoRepository.GetById(Id);
            if (item == null)
            {
                return new ContatoResponseDto();
            }

            var objetoResposta = new ContatoResponseDto
            {
                Idade = DateTime.Now.Year - item.DataNascimento.Year,
                DataNascimento = item.DataNascimento,
                NomeContato = item.NomeContato,
                Telefone = item.Telefone,
                Sexo = item.Sexo,
                Id = item.Id,
                Ativo = item.Ativo
            };

            if (DateTime.Now.DayOfYear < item.DataNascimento.DayOfYear)
            {
                objetoResposta.Idade--;
            }

            return objetoResposta;
        }
    }
}
