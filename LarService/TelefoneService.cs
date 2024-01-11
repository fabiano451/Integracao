using AutoMapper;
using GEntities.Entity;
using LarContracts.IRepository;
using LarContracts.IService;
using LarEntities.DTO;
using LarEntities.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LarService
{
    public class TelefoneService : BaseValidadtionService, ITelefoneService
    {
        IMapper _mapper;
        private readonly ILogger<TelefoneService> _logger;
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly BaseValidadtionService _pessoaRepositoryValidacao;
        private readonly IPessoaRepository _pessoaRepository;

        public TelefoneService(ILogger<TelefoneService> logger,
                               IPessoaRepository pessoaRepository,
                               ITelefoneRepository telefoneRepository, 
                               IMapper mapper, 
                               BaseValidadtionService pessoaRepositoryValidacao) : base(pessoaRepository, logger)
        {
            _logger = logger;
            _telefoneRepository = telefoneRepository;
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
            _pessoaRepositoryValidacao = pessoaRepositoryValidacao;
        }

        public List<TelefoneDTO> GetAll()
        {
            try
            {
                var lstTelefone = _telefoneRepository.GetAllTelefone();

                if (lstTelefone?.Count <= 0)
                {
                    return new List<TelefoneDTO>();
                }

                return _mapper.Map<List<TelefoneDTO>>(lstTelefone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.InternalServerError, ex, "Não foi possível obter a lista de telefone.");
                return new List<TelefoneDTO>();
            }
        }

        public TelefoneDTO GetTelefoneId(int IdTelefone)
        {
            try
            {
                var telefone = _telefoneRepository.FindById(IdTelefone);

                if (telefone is null)
                {
                    AddSystemMessage(HttpStatusCode.InternalServerError, "Não foi possível obter a lista de telefone.");
                    return new TelefoneDTO();
                }

                return _mapper.Map<TelefoneDTO>(telefone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.InternalServerError, ex, "Não foi possível obter o telefone.");
                return new TelefoneDTO();
            }
        }

        public TelefoneDTO AddTelefone(TelefoneDTO telefoneDTO)
        {
            try
            {
                if (telefoneDTO is null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Não foi possível inserir as informações do telefone.");
                    return new TelefoneDTO();
                }

                _pessoaRepositoryValidacao.ValidaTelefone(telefoneDTO, out List<Exception> validacao);

                if (validacao.Count > 0)
                {
                    throw new AggregateException("Erro ao inserir o telefone", validacao);
                }

                var telefone = new Telefone()
                {
                    NumeroTelefone = telefoneDTO.NumeroTelefone,
                    IdPessoa = telefoneDTO.IdPessoa
                    
                };

                _telefoneRepository.Insert(telefone);

                AddSystemMessage(HttpStatusCode.OK, "Telefone inserido com sucesso!");

                return _mapper.Map<TelefoneDTO>(telefone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.BadRequest, ex, "Falha ao inserir o telefone.");
                return new TelefoneDTO();
            }
        }

        public TelefoneDTO UpdateTelefonePessoa(TelefoneDTO pessoaDTO)
        {
            try
            {
                if (pessoaDTO is null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Não foi possível inserir as informações do telefone.");
                    return new TelefoneDTO();
                }

                _pessoaRepositoryValidacao.ValidaTelefone(pessoaDTO, out List<Exception> validacao);

                if (validacao.Count > 0)
                {
                    throw new AggregateException("Erro ao atualizar as informações do telefone", validacao);
                }

                var telefone = _telefoneRepository.FindById(pessoaDTO.IdTelefone);

                if (telefone == null)
                {
                    AddSystemMessage(HttpStatusCode.BadRequest, "Pessoa não encontrada!");
                }

                telefone.NumeroTelefone = pessoaDTO.NumeroTelefone;

                _telefoneRepository.Update(telefone);   

                AddSystemMessage(HttpStatusCode.OK, "Telefone atualizado com sucesso!");

                return _mapper.Map<TelefoneDTO>(telefone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                AddSystemMessage(HttpStatusCode.BadRequest, ex, ex.Message);
                return new TelefoneDTO();
            }
        }

    }
}

