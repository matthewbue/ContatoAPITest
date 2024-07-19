using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Contatos.Application.Dtos;
using Contatos.Application.Services;
using Contatos.Domain.Entities;
using Contatos.Domain.Interface;

namespace Contatos.Application.Tests.Services
{
    public class ContatoServiceTests
    {
        private readonly ContatoService _contatoService;
        private readonly Mock<IContatoRepository> _mockContatoRepository;

        public ContatoServiceTests()
        {
            _mockContatoRepository = new Mock<IContatoRepository>();
            _contatoService = new ContatoService(_mockContatoRepository.Object);
        }

        [Fact]
        public void CreateContato_ValidDto_ReturnsSuccess()
        {
            var dto = new ContatoRequestDto
            {
                DataNascimento = new DateTime(2000, 1, 1),
                NomeContato = "John Doe",
                Telefone = "123456789",
                Sexo = "M"
            };

           
            var result = _contatoService.CreateContato(dto);

            Assert.Equal("Sucesso", result);
            _mockContatoRepository.Verify(repo => repo.Create(It.IsAny<Contato>()), Times.Once);
        }

        [Fact]
        public void UpdateContato_ValidDto_ReturnsSuccess()
        {
            var dto = new ContatoUpdateDto
            {
                Id = 1,
                DataNascimento = new DateTime(2000, 1, 1),
                NomeContato = "John Doe Updated",
                Telefone = "987654321",
                Sexo = "M"
            };

            var contato = new Contato
            {
                Id = 1,
                DataNascimento = new DateTime(2000, 1, 1),
                NomeContato = "Old Name",
                Telefone = "123456789",
                Sexo = "M",
                Ativo = true
            };

            _mockContatoRepository.Setup(repo => repo.GetById(1)).Returns(contato);

            var result = _contatoService.UpdateContato(dto);

            Assert.Equal("Sucesso", result);
            _mockContatoRepository.Verify(repo => repo.Update(It.IsAny<Contato>()), Times.Once);
        }

        [Fact]
        public void GetById_ValidId_ReturnsContatoResponseDto()
        {
            var contato = new Contato
            {
                Id = 1,
                NomeContato = "John Doe",
                Telefone = "123456789",
                Sexo = "M",
                DataNascimento = new DateTime(2000, 1, 1),
                Ativo = true
            };

            _mockContatoRepository.Setup(repo => repo.GetById(1)).Returns(contato);

            var result = _contatoService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("John Doe", result.NomeContato);
            Assert.Equal("123456789", result.Telefone);
        }

        [Fact]
        public void CreateContato_IdadeNaoPodeSerIgualZero_ReturnsErro()
        {
            var dto = new ContatoRequestDto
            {
                DataNascimento = DateTime.Now,
                NomeContato = "John Doe",
                Telefone = "123456789",
                Sexo = "M"
            };

            var result = _contatoService.CreateContato(dto);

            Assert.Equal("A idade não pode ser igual a 0.", result);
        }

        [Fact]
        public void CreateContato_DataNascimentoNaoPodeSerMaior_ReturnsErro()
        {
            var dto = new ContatoRequestDto
            {
                DataNascimento = DateTime.Now.AddDays(20),
                NomeContato = "John Doe",
                Telefone = "123456789",
                Sexo = "M"
            };

            var result = _contatoService.CreateContato(dto);

            Assert.Equal("A data de nascimento não pode ser maior que a data de hoje.", result);
        }
    }
}
