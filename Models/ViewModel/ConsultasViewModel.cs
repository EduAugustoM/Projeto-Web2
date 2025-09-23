using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModel
{
    public class ConsultasViewModel
    {
        [Display(Name = "Médico")]
        public int codm { get; set; }

        [Display(Name = "Paciente")]
        public int codp { get; set; }

        [Display(Name = "Data")]
        public DateTime data { get; set; }

        [Display(Name = "Hora")]
        public TimeSpan hora { get; set; }

        [ValidateNever]
        public List<SelectListItem> Medicos { get; set; }
        [ValidateNever]
        public List<SelectListItem> Pacientes { get; set; }
    }
}
