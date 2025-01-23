using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao : IEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool IsActive { get; set; }
        public List<RegiaoCidade> RegiaoCidades { get; set; }
    }
}