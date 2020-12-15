using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace webapp.Models
{
    [Table("businesscode_suborg")]
    public partial class BusinesscodeSuborg
    {
        [Key]
        [Column("businesscode")]
        [StringLength(255)]
        public string Businesscode { get; set; }
        [Key]
        [Column("suborgnr")]
        public int Suborgnr { get; set; }

        [ForeignKey(nameof(Businesscode))]
        [InverseProperty(nameof(Businesscodes.BusinesscodeSuborg))]
        public virtual Businesscodes BusinesscodeNavigation { get; set; }
        [ForeignKey(nameof(Suborgnr))]
        [InverseProperty(nameof(Suborganizations.BusinesscodeSuborg))]
        public virtual Suborganizations SuborgnrNavigation { get; set; }
    }
}
