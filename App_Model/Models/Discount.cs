using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace App_Model.Models
{
    [Table("Discount")]
    public partial class Discount
    {
       
        public Discount()
        {
            Id = Guid.NewGuid();
            
        }

        public Guid Id { get; set; }

        public double Percent { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        public virtual ICollection<Product> Product { get; set; }


    }
}
