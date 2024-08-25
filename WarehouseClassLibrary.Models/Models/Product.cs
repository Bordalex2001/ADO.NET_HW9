using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_HW9.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int Quantity { get; set; }
        public decimal СostPrice { get; set; }
        public DateTime SupplyDate { get; set; }
    }
}
