using EcoCar.API.InputModels;
using EcoCar.API.ViewModels;

namespace EcoCar.API.Infra.Repositories
{
    public interface IUserClienteRepository
    {
        Task<IEnumerable<UserClienteViewModel>> GetAll();
        Task<UserClienteViewModel> GetById(int id);
        Task<bool> Create(UserClienteInputModel clienteModel);
        Task<bool> Delete(int id);
    }
}