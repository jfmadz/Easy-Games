using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Easy_Games.Models
{
    public class TransactionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 TransactionTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionTypeName { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }

    }
}