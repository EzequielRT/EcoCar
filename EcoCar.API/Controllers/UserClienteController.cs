using EcoCar.API.InputModels;
using EcoCar.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcoCar.API.Controllers
{
    [Route("api/clientes")]
    public class UserClienteController : Controller
    {
        private readonly IUserClienteService _userClienteService;

        public UserClienteController(IUserClienteService userClienteService)
        {
            _userClienteService = userClienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userClienteService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userClienteService.GetById(id);

            if (result == null)
            {
                return Ok(new { Message = "Cliente não encontrado" });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserClienteInputModel inputModel)
        {
            var result = await _userClienteService.Create(inputModel);

            if (result == null)
            {
                return BadRequest(new { Message = "Ocorreu um erro ao registrar o cliente" });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userClienteService.Delete(id);

            if (result == false)
            {
                return BadRequest(new { Message = "Ocorreu um erro ao deletar o cliente" });
            }

            return Ok();
        }
    }
}