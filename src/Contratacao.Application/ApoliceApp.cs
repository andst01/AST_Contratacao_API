using AutoMapper;
using Contratacao.Application.DTO;
using Contratacao.Application.Interfaces;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;

namespace Contratacao.Application
{
    public class ApoliceApp : AppBase<Apolice, ApoliceDTO>, IApoliceApp
    {
        public ApoliceApp(IRepositorioBase<Apolice> repositorio, 
                          IMapper mapper) : base(repositorio, mapper)
        {
        }
    }
}
