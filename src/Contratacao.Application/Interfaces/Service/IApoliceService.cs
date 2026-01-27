using Contratacao.Application.DTO;
using Contratacao.Application.Request;
using Contratacao.Domain.Entidades;
using Contratacao.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Interfaces.Service
{
    
    public interface IApoliceService
    {
        Task<ApoliceDTO> CriarApoliceAsync(ApoliceRequest apolice);
    }
}
