using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Enums;
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
           

            CreateMap<Apolice, ApoliceDTO>()
             .ForMember(x => x.Mensagem, opt => opt.Ignore())
            .ForMember(dest => dest.StatusProposta, opt => opt.Ignore())
            .ForMember(dest => dest.CodigoStatus,
                       opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()))
            .ForMember(dest => dest.NumeroProposta,
                       opt => opt.MapFrom(src => src.Proposta.NumeroProposta))
            .ForMember(dest => dest.NomeCliente,
                       opt => opt.MapFrom(src => src.Proposta.Cliente.Nome));
        }
    }
}
