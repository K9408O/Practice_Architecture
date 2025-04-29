using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Common.DTO
{
    public class RegisterDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone must be exactly 10 digits")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must contain only digits")]
        public string Phone { get; set; }

        [StringLength(10, ErrorMessage = "Tel cannot exceed 10 characters")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression(@"^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        public DateTime Birthday { get; set; }
    }
}
