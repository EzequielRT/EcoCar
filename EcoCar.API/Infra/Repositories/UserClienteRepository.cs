using Dapper;
using EcoCar.API.Infra.UoW;
using EcoCar.API.InputModels;
using EcoCar.API.ViewModels;
using System.Data.SqlClient;

namespace EcoCar.API.Infra.Repositories
{
    public class UserClienteRepository : IUserClienteRepository
    {
        private readonly IUnitOfWork _uow;

        public UserClienteRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Create(UserClienteInputModel clienteModel)
        {
            try
            {
                var sql = @"INSERT INTO
                                    user_clientes
                                        (Name, Email, Password, Hash)
                                    VALUES
                                        (@Name, @Email, @Password, @Hash);

                                SELECT LAST_INSERT_ID();";

                var id = await _uow.Connection.ExecuteScalarAsync<int>(sql,
                       new
                       {
                           Name = clienteModel.Name,
                           Email = clienteModel.Email,
                           Password = clienteModel.Password,
                           Hash = clienteModel.Hash
                       },
                       _uow.Transaction);

                clienteModel.SetId(id);

                return true;
            }
            catch (SqlException)
            {
                _uow.Transaction.Rollback();
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var sql = @"DELETE FROM
                                    user_clientes
                                WHERE
                                    UserId = @UserId;";

                var result = await _uow.Connection.ExecuteAsync(sql,
                       new { UserId = id },
                       _uow.Transaction);

                return result > 0;
            }
            catch (SqlException)
            {
                _uow.Transaction.Rollback();
                return false;
            }
        }

        public async Task<IEnumerable<UserClienteViewModel>> GetAll()
        {
            var sql = "SELECT UserId, Name, Email, Password, Hash FROM user_clientes";

            var clientes = await _uow.Connection.QueryAsync<UserClienteViewModel>(sql);

            return clientes;
        }

        public async Task<UserClienteViewModel> GetById(int id)
        {
            var sql = "SELECT UserId, Name, Email, Password, Hash FROM user_clientes where UserId = @Id";

            var cliente = await _uow.Connection.QueryFirstOrDefaultAsync<UserClienteViewModel>(sql, new { Id = id });

            return cliente;
        }
    }
}