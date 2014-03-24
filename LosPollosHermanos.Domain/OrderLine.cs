using System.ComponentModel.DataAnnotations.Schema;

namespace LosPollosHermanos.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}