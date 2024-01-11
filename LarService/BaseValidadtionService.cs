using LarContracts.IRepository;
using LarContracts.IService;
using LarEntities.DTO;
using LarEntities.ParameterDTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarService
{
    public class BaseValidadtionService : BaseService , IBaseService
    {
        protected readonly IPessoaRepository _pessoaRepositoryValidacao;
        public BaseValidadtionService(IPessoaRepository pessoaRepositoryValidacao, ILogger<BaseValidadtionService> logger) : base(logger)
        {
            _pessoaRepositoryValidacao = pessoaRepositoryValidacao;
        }

        public void ValidaPessoa(PessoaParamDTO pessoaParamDTO, out List<Exception> validacao)
        {
            validacao = new List<Exception>();
           
            if (pessoaParamDTO == null)
            {
                validacao.Add(new Exception(string.Format($"Todos os campos são obrigatórios!")));
                return;
            }
            else if (string.IsNullOrEmpty(pessoaParamDTO.CPF))
            {
                validacao.Add(new Exception(string.Format($"CPF é obrigatório!")));
                return;
            }
            else if (pessoaParamDTO.CPF.Length > 11)
            {
                validacao.Add(new Exception(string.Format($"CPF não esta de acordo!")));
                return;
            }
            else if (string.IsNullOrEmpty(pessoaParamDTO.DataNascimento.ToString()))
            {
                validacao.Add(new Exception(string.Format($"Data de nascimento é obrigatório!")));
            }
        }

        public void ValidaPessoaUpdate(PessoaParamUpdateDTO pessoaParamUpdateDTO, out List<Exception> validacao)
        {
            validacao = new List<Exception>();

            if (pessoaParamUpdateDTO == null)
            {
                validacao.Add(new Exception(string.Format($"Todos os campos são obrigatórios!")));
                return;
            }
            else if (string.IsNullOrEmpty(pessoaParamUpdateDTO.CPF))
            {
                validacao.Add(new Exception(string.Format($"CPF é obrigatório!")));
                return;
            }
            else if (pessoaParamUpdateDTO.CPF.Length > 11)
            {
                validacao.Add(new Exception(string.Format($"CPF não esta de acordo!")));
                return;
            }
            else if (string.IsNullOrEmpty(pessoaParamUpdateDTO.DataNascimento.ToString()))
            {
                validacao.Add(new Exception(string.Format($"Data de nascimento é obrigatório!")));
            }
        }

        public void ValidaTelefone(TelefoneDTO telefoneDTO, out List<Exception> validacao)
        {
            validacao = new List<Exception>();

            if (telefoneDTO == null)
            {
                validacao.Add(new Exception(string.Format($"Todos os campos são obrigatórios!")));
                return;
            }
            else if (string.IsNullOrEmpty(telefoneDTO.NumeroTelefone))
            {
                validacao.Add(new Exception(string.Format($"Telefone é obrigatório!")));
                return;
            }
            else if (telefoneDTO.NumeroTelefone.Length > 11)
            {
                validacao.Add(new Exception(string.Format($"Telefone não esta de acordo!")));
                return;
            }
        }
    }
}
