using System.Collections.Generic;
using Fretefy.Test.Domain.Dto;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        bool Create(RegiaoDto regiao);
        bool Update(RegiaoDto regiao);
        Regiao GetByName(string name);
        List<Regiao> GetAll();
    }
}
