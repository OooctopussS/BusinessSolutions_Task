using Business.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Business.WebApi.Models
{
    public class OrderFilterVm
    {
        public IEnumerable<Order>? Orders { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        [DisplayName("Номера заказов")]
        public MultiSelectList? Numbers { get; set; }

        [DisplayName("Поставщики")]
        public MultiSelectList? Providers { get; set; }

        [DisplayName("Имена элементов заказа")]
        public MultiSelectList? Names { get; set; }

        [DisplayName("Units элементов заказа")]
        public MultiSelectList? Units { get; set; }

        public IEnumerable<string>? NumbersValue { get; set; }
        public IEnumerable<int>? ProvidersValue { get; set; }
        public IEnumerable<string>? NamesValue { get; set; }
        public IEnumerable<string>? UnitsValue { get; set; }
    }
}
