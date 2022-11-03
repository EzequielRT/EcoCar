using EcoCar.API.InputModels;
using EcoCar.API.ViewModels;

namespace EcoCar.API.Service
{
    public interface IUserClienteService
    {
        Task<IEnumerable<UserClienteViewModel>> GetAll();
        Task<UserClienteViewModel> GetById(int id);
        Task<UserClienteInputModel> Create(UserClienteInputModel clienteModel);
        Task<bool> Delete(int id);
    }
}