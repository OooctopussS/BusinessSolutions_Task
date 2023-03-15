using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }

        public string? Name { get; set; }

       [DisplayFormat(DataFormatString="{0:0.000}")]
        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }
    }
}
