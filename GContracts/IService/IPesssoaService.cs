using LarEntities.DTO;
using LarEntities.ParameterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarContracts.IService
{
    public interface IPesssoaService : IBaseService
    {
        List<PessoaParamDTO> GetAll();

        PessoaParamDTO GetPessoaId(int IdPessoa);

        PessoaParamDTO AddPessoa(PessoaParamDTO pessoaParamDTO);

        PessoaParamUpdateDTO UpdatePessoa(PessoaParamUpdateDTO pessoaParamUpdateDTO);

        PessoaDTO DeletePessoaId(int pessoaId);
    }
}
