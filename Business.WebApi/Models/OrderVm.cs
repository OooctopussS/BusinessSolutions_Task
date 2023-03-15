using Business.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Business.WebApi.Models
{
    public class OrderVm
    {
        public Order Order { get; set; } = null!;

        [Display(Name = "Поставщик")]
        public IEnumerable<SelectListItem>? Providers { get; set; }
    }
}
