using Fretefy.Test.Domain.Entities;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Dto
{
    public class RegiaoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public bool IsActive { get; set; }
        public List<Cidade> Cidades { get; set; }
    }
}
