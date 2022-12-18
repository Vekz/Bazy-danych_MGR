using Microsoft.EntityFrameworkCore;

namespace VegeShama.Common.DatabaseModels.EFCore
{
    [PrimaryKey(nameof(ProductId), nameof(OrderId))]
    public class Order_Product
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
