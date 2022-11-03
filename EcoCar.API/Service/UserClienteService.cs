using EcoCar.API.Infra.Repositories;
using EcoCar.API.Infra.UoW;
using EcoCar.API.InputModels;
using EcoCar.API.ViewModels;

namespace EcoCar.API.Service
{
    public class UserClienteService : IUserClienteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserClienteRepository _userClienteRepository;

        public UserClienteService(IUnitOfWork uow, IUserClienteRepository userClienteRepository)
        {
            _uow = uow;
            _userClienteRepository = userClienteRepository;
        }

        public async Task<UserClienteInputModel> Create(UserClienteInputModel clienteModel)
        {
            using (_uow.Begin())
            {
                await _userClienteRepository.Create(clienteModel);
                _uow.Commit();
            }

            return clienteModel;
        }

        public async Task<bool> Delete(int id)
        {
            bool result;

            using (_uow.Begin())
            {
                result = await _userClienteRepository.Delete(id);
                _uow.Commit();
            }

            return result;
        }

        public async Task<IEnumerable<UserClienteViewModel>> GetAll()
        {
            return await _userClienteRepository.GetAll();
        }

        public async Task<UserClienteViewModel> GetById(int id)
        {
            return await _userClienteRepository.GetById(id);
        }
    }
}
