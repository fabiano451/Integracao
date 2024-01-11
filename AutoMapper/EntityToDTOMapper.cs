using AutoMapper;
using GEntities.Entity;
using LarEntities.DTO;
using LarEntities.Entity;
using LarEntities.ParameterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarAutoMapper
{
    public class EntityToDTOMapper : Profile
    {

        public EntityToDTOMapper()
        {
            CreateMap<Pessoa, PessoaDTO>()
                .ForMember(target => target.IdPessoa, source => source.MapFrom(result => result.IdPessoa))
                .ForMember(target => target.CPF, source => source.MapFrom(result => result.CPF))
                .ForMember(target => target.DataNascimento, source => source.MapFrom(result => result.DataNascimento))
                .ForMember(target => target.Ativo, source => source.MapFrom(result => result.Ativo))
                .ForMember(target => target.Telefone, source => source.MapFrom(result => result.Telefone))
               .ForMember(target => target.Nome, source => source.MapFrom(result => result.Nome));

            CreateMap<Pessoa, PessoaParamDTO>()
               .ForMember(target => target.IdPessoa, source => source.MapFrom(result => result.IdPessoa))
               .ForMember(target => target.Nome, source => source.MapFrom(result => result.Nome))
               .ForMember(target => target.CPF, source => source.MapFrom(result => result.CPF))
               .ForMember(target => target.DataNascimento, source => source.MapFrom(result => result.DataNascimento))
               .ForMember(target => target.Ativo, source => source.MapFrom(result => result.Ativo))
               .ForMember(target => target.Telefones, source => source.MapFrom(result => result.Telefone));



            CreateMap<Pessoa, PessoaParamUpdateDTO>()
              .ForMember(target => target.IdPessoa, source => source.MapFrom(result => result.IdPessoa))
              .ForMember(target => target.Nome, source => source.MapFrom(result => result.Nome))
              .ForMember(target => target.CPF, source => source.MapFrom(result => result.CPF))
              .ForMember(target => target.DataNascimento, source => source.MapFrom(result => result.DataNascimento))
              .ForMember(target => target.Ativo, source => source.MapFrom(result => result.Ativo));
             // .ForMember(target => target.Telefones, source => source.MapFrom(result => result.Telefone));
             

            CreateMap<Telefone, TelefoneDTO>()
                .ForMember(target => target.IdTelefone, source => source.MapFrom(result => result.IdTelefone))
                .ForMember(target => target.NumeroTelefone, source => source.MapFrom(result => result.NumeroTelefone))
                .ForMember(target => target.Tipo, source => source.MapFrom(result => result.Tipo));

            CreateMap<Telefone, TelefoneParamDTO>()
               .ForMember(target => target.IdTelefone, source => source.MapFrom(result => result.IdTelefone))
               .ForMember(target => target.IdPessoa, source => source.MapFrom(result => result.IdPessoa))
               .ForMember(target => target.NumeroTelefone, source => source.MapFrom(result => result.NumeroTelefone))
               .ForMember(target => target.Tipo, source => source.MapFrom(result => result.Tipo));

        }
    }
}
