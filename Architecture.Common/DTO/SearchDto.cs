using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Common.DTO
{
    public class SearchDto
    {
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
    }
}
