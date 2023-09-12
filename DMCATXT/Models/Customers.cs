using System.ComponentModel.DataAnnotations.Schema;

namespace DMCATXT.Models
{
    [Table("Customer_Table")]
    public class Customers
    {
        public int CustomerID { get; set; }
        public string? CustomerName { get; set; }
    }
}
