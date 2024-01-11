using AutoMapper;
using GEntities.Entity;
using LarContracts.IRepository;
using LarContracts.IService;
using LarEntities.DTO;
using LarEntities.Entity;
using LarEntities.ParameterDTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LarService
{
    public class PessoaService : BaseValidadtionService, IPesssoaService
    {
        IMapper _mapper;
        private readonly ILogger<PessoaService> _logger;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly BaseValidadtionService _pessoaRepositoryValidacao;
        private readonly ITelefoneRepository _telefoneRepository;

        public PessoaService(ILogger<PessoaService> logger, 
                             IPessoaRepository pessoaRepository, ITelefoneRepository telefoneRepository, IMapper mapper, BaseValidadtionService pessoaRepositoryValidacao) : base(pessoaRepository, logger)
        {
            _logger = logger;
            _pessoaRepository = pessoaRepository;
            _telefoneRepository = telefoneRepository;
            _mapper = mapper;
            _pessoaRepositoryValidacao = pessoaRepositoryValidacao;
        }

        public List<PessoaParamDTO> GetAll()
        {
            try
            {
                var lstPessoa = _pessoaRepository.GetAllPessoa().Where(x => x.Ativo.Equals(true)).ToList();

                if (lstPessoa?.Count <= 0)
                {
                    return new List<PessoaParamDTO>();
                }

                return _mapper.Map<List<PessoaParamDTO>>(lstPessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.InternalServerError, ex, "Não foi possível obter a lista de pessoa.");
                return new List<PessoaParamDTO>();
            }
        }

        public PessoaParamDTO GetPessoaId(int IdPessoa)
        {
            try
            {
                var pessoa = _pessoaRepository.FindById(IdPessoa);

                if (pessoa is null)
                {
                    AddSystemMessage(HttpStatusCode.InternalServerError,  "Não foi possível obter a lista de pessoa.");
                    return new PessoaParamDTO();
                }

                return _mapper.Map<PessoaParamDTO>(pessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.InternalServerError, ex, "Não foi possível obter a pessoa.");
                return new PessoaParamDTO();
            }
        }

        public PessoaParamDTO AddPessoa(PessoaParamDTO pessoaParamDTO)
        {
            try
            {

                if (pessoaParamDTO is null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Não foi possível inserir as informações da pessoa.");
                    return new PessoaParamDTO();
                }

                _pessoaRepository.BeginTransaction();

                _pessoaRepositoryValidacao.ValidaPessoa(pessoaParamDTO, out List<Exception> validacao);

                if(validacao.Count > 0)
                {
                    throw new AggregateException("Erro ao inserir a pessoa", validacao);
                }

                var pessoa = new Pessoa()
                {
                    Nome = pessoaParamDTO.Nome,
                    DataNascimento = pessoaParamDTO.DataNascimento,
                    CPF = pessoaParamDTO.CPF,
                    Ativo = true
                };

                var response = _pessoaRepository.Insert(pessoa).IdPessoa;
                foreach (var item in pessoaParamDTO.Telefones)
                {
                    var telefone = new Telefone()
                    {
                        IdPessoa = response,
                        Tipo = item.Tipo,
                        NumeroTelefone = item.NumeroTelefone
                    };
                    
                    _telefoneRepository.Insert(telefone);
                }

                _pessoaRepository.CommitTransaction();


                AddSystemMessage(HttpStatusCode.OK, "Pessoa inserida com sucesso!");
               
                return _mapper.Map<PessoaParamDTO>(pessoa);
            }
            catch (Exception ex)
            {
                _pessoaRepository.RollbackTransaction();
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.BadRequest, ex, "Falha ao inserir as informações da pessoa.");
                return new PessoaParamDTO();
            }
        }

        public PessoaParamUpdateDTO UpdatePessoa(PessoaParamUpdateDTO pessoaParamUpdateDTO)
        {
            try
            {
                if (pessoaParamUpdateDTO is null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Não foi possível inserir as informações da pessoa.");
                    return new PessoaParamUpdateDTO();
                }
                _pessoaRepository.BeginTransaction();
                _pessoaRepositoryValidacao.ValidaPessoaUpdate(pessoaParamUpdateDTO, out List<Exception> validacao);

                if (validacao.Count > 0)
                {
                    throw new AggregateException("Erro ao atualizar as informações da pessoa", validacao);
                }

                var pessoa = _pessoaRepository.FindById(pessoaParamUpdateDTO.IdPessoa);

                if(pessoa == null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest,  "Pessoa não encontrada!");
                }

                pessoa.Nome = pessoaParamUpdateDTO.Nome;
                pessoa.CPF = pessoaParamUpdateDTO.CPF;
                pessoa.DataNascimento = pessoaParamUpdateDTO.DataNascimento;
                pessoa.Ativo = pessoaParamUpdateDTO.Ativo;
                
                _pessoaRepository.Update(pessoa);

               

                foreach (var item in pessoaParamUpdateDTO.Telefones)
                {
                    var telefone = _telefoneRepository.FindById(item.TelefoneId);
                    
                    if (telefone != null)
                    {
                        telefone.Tipo = item.Tipo;
                        telefone.NumeroTelefone = item.NumeroTelefone;
                        telefone.IdPessoa = item.IdPessoa;
                        _telefoneRepository.Update(telefone);
                    }

                    
                }

                AddSystemMessage(HttpStatusCode.OK, "Pessoa atualizada com sucesso!");

                return _mapper.Map<PessoaParamUpdateDTO>(pessoa);
            }
            catch (Exception ex)
            {
                _pessoaRepository.RollbackTransaction();
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.BadRequest, ex, ex.Message);
                return new PessoaParamUpdateDTO();
            }
        }

        public PessoaDTO DeletePessoaId(int pessoaId)
        {
            try
            {
                if (pessoaId  == 0)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Por favor, informar o código da pessoa!");
                    return new PessoaDTO();
                }


                var pessoa = _pessoaRepository.FindById(pessoaId);

                if (pessoa == null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Pessoa não encontrada!");
                }

                pessoa.Ativo = false;

                _pessoaRepository.Update(pessoa);

                AddSystemMessage(HttpStatusCode.OK, "Pessoa excluída com sucesso!");

                return _mapper.Map<PessoaDTO>(pessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.BadRequest, ex, ex.Message);
                return new PessoaDTO();
            }
        }
    }
}
