using System;

namespace Fretefy.Test.Domain.Entities
{
    public class RegiaoCidade : IEntity
    {
        public Guid Id { get; set; }
        public Guid RegiaoId { get; set; }
        public Guid CidadeId { get; set; }
        public Regiao Regiao { get; set; }
        public Cidade Cidade { get; set; }
    }
}
