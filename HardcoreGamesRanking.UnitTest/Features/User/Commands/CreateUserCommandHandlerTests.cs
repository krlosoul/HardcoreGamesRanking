namespace HardcoreGamesRanking.UnitTest.Features.User.Commands
{
    using Xunit;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using Business.Features.User.Commands;
    using Core.Entities;
    using Business.Interfaces.DataAccess;
    using AutoMapper;
    using Business.Common.Exceptions;
    using System.Linq.Expressions;

    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_CreatesUserSuccessfully()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "test@example.com",
                Password = "1234",
                Username = "test"
            };

            _unitOfWorkMock.Setup(uow => uow.UsuarioRepository.AnyAsync(It.IsAny<Expression<Func<Usuario, bool>>>())) .ReturnsAsync(false); 

            _mapperMock.Setup(m => m.Map<Usuario>(It.IsAny<CreateUserCommand>()))
                .Returns(new Usuario 
                { 
                    Id = 1,
                    Email = command.Email,
                    Username = command.Username,
                    Password = command.Password,
                    FechaRegistro = DateTime.Now
                }); 

            _unitOfWorkMock.Setup(uow => uow.UsuarioRepository.InsertAsync(It.IsAny<Usuario>())).ReturnsAsync(true); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Se creo el usuario con el id 1 correctamente.", result.Message);
            _unitOfWorkMock.Verify(uow => uow.UsuarioRepository.InsertAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserAlreadyExists_ThrowsBadRequestException()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "test@example.com",
                Password = "1234",
                Username = "test"
            };

            _unitOfWorkMock.Setup(uow => uow.UsuarioRepository.AnyAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"El correo {command.Email} ya existe.", exception.Message);
        }

        [Fact]
        public async Task Handle_InsertFails_ThrowsBadRequestException()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "test@example.com",
                Password = "1234",
                Username = "test"
            };

            _unitOfWorkMock.Setup(uow => uow.UsuarioRepository.AnyAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(false);

            var usuarioMock = new Usuario
            {
                Id = 1,
                Email = command.Email,
                Password = command.Password,
                Username = command.Username
            };

            _mapperMock.Setup(m => m.Map<Usuario>(It.IsAny<CreateUserCommand>())).Returns(usuarioMock);

            _unitOfWorkMock.Setup(uow => uow.UsuarioRepository.InsertAsync(It.IsAny<Usuario>())).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));

            // Verifica el mensaje de la excepción
            Assert.Equal("No se pudo registrar el usuario en el sistema.", exception.Message);
            _unitOfWorkMock.Verify(uow => uow.UsuarioRepository.InsertAsync(It.IsAny<Usuario>()), Times.Once);
        }
    }

}