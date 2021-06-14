using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Easy_Games.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 TransactionID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [ForeignKey("TransactionType")]
        public Int16 TransactionTypeID { get; set; }

        [Required]
        [ForeignKey("Client")]
        public  int ClientID { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        public virtual Client Client { get; set; }
    }
}