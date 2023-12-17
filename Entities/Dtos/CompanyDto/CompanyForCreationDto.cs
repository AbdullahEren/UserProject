using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.CompanyDto
{
    public record CompanyForCreationDto
    {
        [Required(ErrorMessage = "Company Name is required.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Catch Phrase is required.")]
        public string CatchPhrase { get; init; }

        [Required(ErrorMessage = "Business Description is required.")]
        public string Bs { get; init; }
    }
}
