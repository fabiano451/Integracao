using LarContracts.IService;
using LarEntities.DTO;
using LarEntities.ParameterDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WepApi.Controllers
{
    [Route("/pessoa")]
    [ApiController]
    public class PessoaController : BaseController
    {
        private readonly IPesssoaService _pessoaService;
        private readonly ILogger<PessoaController> _logger;


        public PessoaController(IPesssoaService pessoaService, 
                                ILogger<PessoaController> logger
                               ) : base(logger)
        {
            _pessoaService = pessoaService;
            _logger = logger;
        }

        #region Get

        [HttpGet]
        //[Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var result = _pessoaService.GetAll();

                return TreatOkResult(result, _pessoaService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }


        [HttpGet("{id}")]
      //  [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _pessoaService.GetPessoaId(id);

                if (_pessoaService.MessageList.Count() > 0)
                {
                    return InternalServerError(_pessoaService.MessageList[0].Detail);
                }

                return TreatOkResult(result, _pessoaService);
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
        public IActionResult Post([FromBody] PessoaParamDTO pessoaParamDTO)
        {
            try
            {
                var result = _pessoaService.AddPessoa(pessoaParamDTO);

                var _msg = _pessoaService.MessageList;

                if (_msg[0].Type.ToString().Contains("BadRequest"))
                    return BadRequest(_msg[0].Detail.ToString());

                return TreatOkResult(result, _pessoaService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }

        
        [HttpPut]
        public IActionResult Put([FromBody] PessoaParamUpdateDTO pessoaDTO)
        {
            try
            {
                var result = _pessoaService.UpdatePessoa(pessoaDTO);

                var _msg = _pessoaService.MessageList;

                if (_msg[0].Type.ToString().Contains("BadRequest"))
                    return BadRequest(_msg[0].Detail.ToString());

                return TreatOkResult(result, _pessoaService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }
        #endregion




        
        [HttpDelete]
        public IActionResult Delete(int pessoaId)
        {
            try
            {
                var result = _pessoaService.DeletePessoaId(pessoaId);

                var _msg = _pessoaService.MessageList;

                if (_msg[0].Type.ToString().Contains("BadRequest"))
                    return BadRequest(_msg[0].Detail.ToString());

                return Ok(_pessoaService.MessageList[0].Detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return InternalServerError(ex.Message);
            }
        }
    }
}
