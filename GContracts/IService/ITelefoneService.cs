using LarEntities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarContracts.IService
{
    public interface ITelefoneService : IBaseService
    {
        List<TelefoneDTO> GetAll();

        TelefoneDTO GetTelefoneId(int IdTelefone);

        TelefoneDTO AddTelefone(TelefoneDTO telefoneDTO);

        TelefoneDTO UpdateTelefonePessoa(TelefoneDTO pessoaDTO);



    }
}
