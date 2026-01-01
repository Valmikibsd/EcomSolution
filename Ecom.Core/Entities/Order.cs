using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string UserId { get; set; } = default!;
        public string ShippingAddress { get; set; } = default!;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
