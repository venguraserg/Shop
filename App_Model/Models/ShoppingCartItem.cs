using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Model.Models
{
    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        public ShoppingCartItem()
        {

            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int Count { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? ShoppingCartId { get; set; }

        public float Amount { get; set; }

        public decimal Price { get; set; }

        public virtual Product Product { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}
