using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Domain
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Номер заказа")]
        public string? Number { get; set; }

        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }

        [Display(Name = "Провайдер")]
        public int ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public virtual Provider? Provider { get; set; }

        public virtual IList<OrderItem?>? OrderItems { get; set;}
    }
}
