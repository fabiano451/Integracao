using LarContracts.IService;
using LarEntities.DTO;
using LarService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WepApi.Controllers
{
    [Route("api/Telefone")]
    [ApiController]
    public class TelefoneController : BaseController
    {

        private readonly ITelefoneService _telefoneService;
        private readonly ILogger<TelefoneController> _logger;


        public TelefoneController(ITelefoneService telefoneService,
                                ILogger<TelefoneController> logger
                               ) : base(logger)
        {
           _telefoneService = telefoneService;
            _logger = logger;
        }


        #region Get

        [HttpGet]
        //[Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var result = _telefoneService.GetAll();

                return TreatOkResult(result, _telefoneService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }


        [HttpGet("{id}")]
        //[Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _telefoneService.GetTelefoneId(id);

                if (_telefoneService.MessageList.Count() > 0)
                {
                    return InternalServerError(_telefoneService.MessageList[0].Detail);
                }

                return TreatOkResult(result, _telefoneService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }
        #endregion

        #region Post - Put
        [HttpPost]
        public IActionResult Post([FromBody] TelefoneDTO telefoneDTO)
        {
            try
            {
                var result = _telefoneService.AddTelefone(telefoneDTO);

                var _msg = _telefoneService.MessageList;

                if (_msg[0].Type.ToString().Contains("BadRequest"))
                    return BadRequest(_msg[0].Detail.ToString());

                return TreatOkResult(result, _telefoneService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult Put([FromBody] TelefoneDTO pessoaDTO)
        {
            try
            {
                var result = _telefoneService.UpdateTelefonePessoa(pessoaDTO);

                var _msg = _telefoneService.MessageList;

                if (_msg[0].Type.ToString().Contains("BadRequest"))
                    return BadRequest(_msg[0].Detail.ToString());

                return TreatOkResult(result, _telefoneService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }
        #endregion
    }
}
