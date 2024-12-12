using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Minimal_API.API.Controllers;
using Minimal_API.Application.DTOs;
using Minimal_API.Application.Interfaces;

namespace Minimal_API.Test.Domain.Entities
{
    [TestClass]
    public class VeiculoControllerTests
    {
        private Mock<IVeiculoService> _mockVeiculoService;
        private VeiculoController _controller;
        private VeiculoDTO _veiculoDTO;

        [TestInitialize]
        public void Setup()
        {
            // Configuração inicial para todos os testes
            _mockVeiculoService = new Mock<IVeiculoService>();
            _controller = new VeiculoController(_mockVeiculoService.Object);

            // Simular usuário autorizado (necessário para testes com [Authorize])
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Role, "Adm")
            }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            _veiculoDTO = new VeiculoDTO
            {
                // Preencha com dados de teste
                Id = 1,
                Marca = "TestMarca",
            };
        }

        [TestMethod]
        public void CadastroVeiculos_QuandoSucesso_RetornaOk()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.Incluir(It.IsAny<VeiculoDTO>()))
                .Returns(_veiculoDTO);

            // Act
            var resultado = _controller.CadastroVeiculos(_veiculoDTO) as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(_veiculoDTO, resultado.Value);
        }

        [TestMethod]
        public void CadastroVeiculos_QuandoFalha_RetornaBadRequest()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.Incluir(It.IsAny<VeiculoDTO>()))
                .Returns((VeiculoDTO)null);

            // Act
            var resultado = _controller.CadastroVeiculos(_veiculoDTO) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
        }

        [TestMethod]
        public void RetornaTodosVeiculos_QuandoExistemVeiculos_RetornaOk()
        {
            // Arrange
            var listaVeiculos = new List<VeiculoDTO> { _veiculoDTO };
            _mockVeiculoService
                .Setup(s => s.Todos(It.Is<int?>(p => p == 1), null, null))
                .Returns(listaVeiculos);

            // Act
            var resultado = _controller.RetornaTodosVeiculos(1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(listaVeiculos, resultado.Value);
        }

        [TestMethod]
        public void RetornaTodosVeiculos_QuandoNaoExistemVeiculos_RetornaNotFound()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.Todos(It.Is<int?>(p => p == 1), null, null))
                .Returns((List<VeiculoDTO>)null);

            // Act
            var resultado = _controller.RetornaTodosVeiculos(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [TestMethod]
        public void BuscaPorId_QuandoVeiculoExiste_RetornaOk()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.BuscaPorId(It.IsAny<int>()))
                .Returns(_veiculoDTO);

            // Act
            var resultado = _controller.BuscaPorId(1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(_veiculoDTO, resultado.Value);
        }

        [TestMethod]
        public void BuscaPorId_QuandoVeiculoNaoExiste_RetornaNotFound()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.BuscaPorId(It.IsAny<int>()))
                .Returns((VeiculoDTO)null);

            // Act
            var resultado = _controller.BuscaPorId(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [TestMethod]
        public void EditarVeiculo_QuandoVeiculoExiste_RetornaOk()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.BuscaPorId(It.IsAny<int>()))
                .Returns(_veiculoDTO);

            // Act
            var resultado = _controller.EditarVeiculo(1, _veiculoDTO) as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            _mockVeiculoService.Verify(s => s.Atualizar(It.IsAny<VeiculoDTO>()), Times.Once);
        }

        [TestMethod]
        public void EditarVeiculo_QuandoVeiculoNaoExiste_RetornaNotFound()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.BuscaPorId(It.IsAny<int>()))
                .Returns((VeiculoDTO)null);

            // Act
            var resultado = _controller.EditarVeiculo(1, _veiculoDTO) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [TestMethod]
        public void ExcluirVeiculo_QuandoVeiculoExiste_RetornaNoContent()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.BuscaPorId(It.IsAny<int>()))
                .Returns(_veiculoDTO);

            // Act
            var resultado = _controller.ExcluirVeiculo(1) as NoContentResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(204, resultado.StatusCode);
            _mockVeiculoService.Verify(s => s.Apagar(It.IsAny<VeiculoDTO>()), Times.Once);
        }

        [TestMethod]
        public void ExcluirVeiculo_QuandoVeiculoNaoExiste_RetornaNotFound()
        {
            // Arrange
            _mockVeiculoService
                .Setup(s => s.BuscaPorId(It.IsAny<int>()))
                .Returns((VeiculoDTO)null);

            // Act
            var resultado = _controller.ExcluirVeiculo(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
        }
    }
}
