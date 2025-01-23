using System.Collections.Generic;
using Fretefy.Test.Domain.Dto;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService
    {
        RegiaoDto GetByName(string name);
        List<RegiaoDto> GetAll();
        bool Create(RegiaoDto regiao);
        bool Update(RegiaoDto regiao);
    }
}