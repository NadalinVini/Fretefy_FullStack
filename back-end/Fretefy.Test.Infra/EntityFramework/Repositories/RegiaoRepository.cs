using Fretefy.Test.Domain.Dto;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<Regiao> _dbSet;

        public RegiaoRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Regiao>();
        }

        public bool Create(RegiaoDto regiaoDto)
        {
            var exists = _dbSet.FirstOrDefault(r => r.Nome == regiaoDto.Nome);
            if (exists == null)
            {
                var guid = Guid.NewGuid();
                var regiao = new Regiao
                {

                    Id = guid,
                    Nome = regiaoDto.Nome,
                    IsActive = true,
                    RegiaoCidades = regiaoDto.Cidades.Select(cidadeId => new RegiaoCidade
                    {
                        Id = Guid.NewGuid(),
                        RegiaoId = guid,
                        CidadeId = cidadeId.Id
                    }).ToList()
                };
                _dbSet.Add(regiao);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Regiao GetByName(string name)
        {
            return _dbSet
                .Include(r => r.RegiaoCidades)
                .ThenInclude(rc => rc.Cidade)
                .FirstOrDefault(r => r.Nome == name);
        }

        public List<Regiao> GetAll()
        {
            return _dbSet
                .Include(r => r.RegiaoCidades)
                .ThenInclude(rc => rc.Cidade)
                .ToList();
        }

        public bool Update(RegiaoDto regiaoDto)
        {
            var regiao = _dbSet.Include(r => r.RegiaoCidades).FirstOrDefault(r => r.Id == Guid.Parse(regiaoDto.Id));

            if (regiao == null) return false;

            var novosIdsCidades = regiaoDto.Cidades.Select(c => c.Id).ToHashSet();

            regiao.RegiaoCidades.RemoveAll(rc => !novosIdsCidades.Contains(rc.CidadeId));

            foreach (var cidadeDto in regiaoDto.Cidades)
            {
                if (!regiao.RegiaoCidades.Any(rc => rc.CidadeId == cidadeDto.Id))
                {
                    var novaRegiaoCidade = new RegiaoCidade
                    {
                        RegiaoId = regiao.Id,
                        CidadeId = cidadeDto.Id
                    };
                    regiao.RegiaoCidades.Add(novaRegiaoCidade);
                }
            }

            regiao.Nome = regiaoDto.Nome;
            regiao.IsActive = regiaoDto.IsActive;

            _dbContext.SaveChanges();

            return true;
        }
    }
}