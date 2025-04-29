using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Common.DTO
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Tel { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}
