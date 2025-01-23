using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Dto;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;
        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public bool Create(RegiaoDto regiao)
        {
            _regiaoRepository.Create(regiao);
            return true;
        }
        public bool Update(RegiaoDto regiao)
        {            
            return _regiaoRepository.Update(regiao);
        }

        public List<RegiaoDto> GetAll()
        {
            var regionList = _regiaoRepository.GetAll();
            return MapRegioesToDto(regionList);
        }

        public RegiaoDto GetByName(string name)
        {
            var regiao = _regiaoRepository.GetByName(name);
            return MapRegioesToDto(new List<Regiao> { regiao }).FirstOrDefault();
        }

        private List<RegiaoDto> MapRegioesToDto(List<Regiao> regioes)
        {
            return regioes.Select(regiao => new RegiaoDto
            {
                Id = regiao.Id.ToString(),
                Nome = regiao.Nome,
                IsActive = regiao.IsActive,
                Cidades = regiao.RegiaoCidades.Select(rc => new Cidade
                {
                    Id = rc.Cidade.Id,
                    Nome = rc.Cidade.Nome,
                    UF = rc.Cidade.UF
                }).ToList()
            }).ToList();
        }
    }
}