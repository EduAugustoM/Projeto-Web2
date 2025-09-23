using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModel
{
    public class AmbulatoriosViewModel
    {
        [Display(Name = "Número do Ambulatório")]
        public int nroa { get; set; }

        [Display(Name = "Andar")]
        public int andar { get; set; }

        [Display(Name = "Capacidade")]
        public int capacidade { get; set; }

        [ValidateNever]
        public List<SelectListItem> Medicos { get; set; }
    }
}