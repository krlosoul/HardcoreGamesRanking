namespace HardcoreGamesRanking.Infrastructure.DataAccess
{
    using Business.Interfaces.DataAccess;
    using Core.Entities;
    using HardcoreGamesRanking.Infrastructure.DirectorioDestino;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Threading.Tasks;

    public class UnitOfWork(HardcoreGamesRankingContext dbContext) : IUnitOfWork
    {
        #region Properties
        private DbContext DbContext { get; set; } = dbContext;
        private IDbContextTransaction? _transaction;
        private IRepository<Calificacion>? _calificacionRepository;
        private IRepository<Compania>? _companiaRepository;
        private IRepository<Usuario>? _usuarioRepository;
        private IRepository<Videojuego>? _videojuegoRepository;
        #endregion

        #region Transactions
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await SaveAsync();
            }
        }

        public async Task CloseTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }
        #endregion

        #region Repositories
        public IRepository<Calificacion> CalificacionRepository
        {
            get
            {
                return _calificacionRepository ??= new Repository<Calificacion>(DbContext);
            }
        }
        public IRepository<Compania> CompaniaRepository
        {
            get
            {
                return _companiaRepository ??= new Repository<Compania>(DbContext);
            }
        }
        public IRepository<Usuario> UsuarioRepository
        {
            get
            {
                return _usuarioRepository ??= new Repository<Usuario>(DbContext);
            }
        }
        public IRepository<Videojuego> VideojuegoRepository
        {
            get
            {
                return _videojuegoRepository ??= new Repository<Videojuego>(DbContext);
            }
        }
        #endregion

        #region Private Methods
        private async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
        #endregion
    }
}