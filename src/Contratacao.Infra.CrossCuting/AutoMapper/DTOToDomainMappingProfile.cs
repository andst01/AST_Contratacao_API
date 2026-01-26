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
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<ApoliceDTO, Apolice>()
                .ForMember(x => x.Proposta, opt => opt.Ignore());
        }
    }
}
