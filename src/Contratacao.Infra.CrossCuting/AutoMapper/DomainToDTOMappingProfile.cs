using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.CrossCuting.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            //CreateMap<Apolice, ApoliceDTO>();

            CreateMap<Apolice, ApoliceDTO>()
            .ForMember(dest => dest.NumeroProposta,
                       opt => opt.MapFrom(src => src.Proposta.NumeroProposta))
            .ForMember(dest => dest.NomeCliente,
                       opt => opt.MapFrom(src => src.Proposta.Cliente.Nome));
        }
    }
}
