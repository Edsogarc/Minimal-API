using Microsoft.AspNetCore.Mvc;
using Moq;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Account;
using Minimal_API.API.Controllers;
using Minimal_API.Application.DTOs;

[TestClass]
public class UsuarioControllerTests
{
    private Mock<IUsuarioService> _mockUsuarioService;
    private Mock<IAuthenticate> _mockAuthenticate;
    private UsuarioController _controller;

    [TestInitialize]
    public void Setup()
    {
        // Configuração inicial para cada teste
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockAuthenticate = new Mock<IAuthenticate>();
        _controller = new UsuarioController(_mockUsuarioService.Object, _mockAuthenticate.Object);
    }

    [TestMethod]
    public void LoginUsuario_UsuarioNaoExiste_RetornaUnauthorized()
    {
        // Arrange
        var login = new Login { Email = "teste@exemplo.com", Password = "senha123" };
        _mockAuthenticate.Setup(a => a.UserExists(login.Email)).Returns(false);

        // Act
        var result = _controller.LoginUsario(login);

        // Assert
        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        var unauthorizedResult = result as UnauthorizedObjectResult;
        Assert.AreEqual("Usuário não existe", unauthorizedResult.Value);
    }

    [TestMethod]
    public void LoginUsuario_CredenciaisInvalidas_RetornaUnauthorized()
    {
        // Arrange
        var login = new Login { Email = "teste@exemplo.com", Password = "senha123" };
        _mockAuthenticate.Setup(a => a.UserExists(login.Email)).Returns(true);
        _mockAuthenticate.Setup(a => a.Authenticate(login.Email, login.Password)).Returns(false);

        // Act
        var result = _controller.LoginUsario(login);

        // Assert
        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        var unauthorizedResult = result as UnauthorizedObjectResult;
        Assert.AreEqual("Usuário ou senha inválida", unauthorizedResult.Value);
    }

    [TestMethod]
    public void LoginUsuario_CredenciaisValidas_RetornaToken()
    {
        // Arrange
        var login = new Login { Email = "teste@exemplo.com", Password = "senha123" };
        var usuario = new UsuarioDTO
        {
            Id = 1,
            Email = login.Email,
            Perfil = "Administrador"
        };

        _mockAuthenticate.Setup(a => a.UserExists(login.Email)).Returns(true);
        _mockAuthenticate.Setup(a => a.Authenticate(login.Email, login.Password)).Returns(true);
        _mockUsuarioService.Setup(a => a.BuscaPorEmail(login.Email)).Returns(usuario);
        _mockAuthenticate.Setup(a => a.GenerateToken(usuario.Id, usuario.Email, usuario.Perfil))
            .Returns("token_jwt_exemplo");

        // Act
        var result = _controller.LoginUsario(login);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsTrue(okResult.Value.ToString().Contains("Token:"));
    }

    [TestMethod]
    public void CadastroUsuario_EmailJaExistente_RetornaBadRequest()
    {
        // Arrange
        var usuarioDTO = new UsuarioDTO { Email = "teste@exemplo.com" };
        _mockAuthenticate.Setup(a => a.UserExists(usuarioDTO.Email)).Returns(true);

        // Act
        var result = _controller.CadastroUsuario(usuarioDTO);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Esse E-mail já possui cadastro", badRequestResult.Value);
    }

    [TestMethod]
    public void CadastroUsuario_CadastroComSucesso_RetornaCreated()
    {
        // Arrange
        var usuarioDTO = new UsuarioDTO
        {
            Id = 1,
            Email = "teste@exemplo.com"
        };
        _mockAuthenticate.Setup(a => a.UserExists(usuarioDTO.Email)).Returns(false);
        _mockUsuarioService.Setup(s => s.Incluir(usuarioDTO)).Returns(usuarioDTO);

        // Act
        var result = _controller.CadastroUsuario(usuarioDTO);

        // Assert
        Assert.IsInstanceOfType(result, typeof(CreatedResult));
        var createdResult = result as CreatedResult;
        Assert.AreEqual(usuarioDTO, createdResult.Value);
    }

    [TestMethod]
    public void BuscaPorId_UsuarioExistente_RetornaOk()
    {
        // Arrange
        int usuarioId = 1;
        var usuarioDTO = new UsuarioDTO
        {
            Id = usuarioId,
            Email = "teste@exemplo.com"
        };
        _mockUsuarioService.Setup(s => s.BuscaPorId(usuarioId)).Returns(usuarioDTO);

        // Act
        var result = _controller.BuscaPorId(usuarioId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.AreEqual(usuarioDTO, okResult.Value);
    }

    [TestMethod]
    public void BuscaPorId_UsuarioNaoExistente_RetornaNotFound()
    {
        // Arrange
        int usuarioId = 999;
        _mockUsuarioService.Setup(s => s.BuscaPorId(usuarioId)).Returns((UsuarioDTO)null);

        // Act
        var result = _controller.BuscaPorId(usuarioId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}