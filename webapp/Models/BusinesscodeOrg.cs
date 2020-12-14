using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace webapp.Models
{
    [Table("businesscode_org")]
    public partial class BusinesscodeOrg
    {
        [Key]
        [Column("businesscode")]
        [StringLength(255)]
        public string Businesscode { get; set; }
        [Key]
        [Column("orgnr")]
        public int Orgnr { get; set; }

        [ForeignKey(nameof(Businesscode))]
        [InverseProperty("BusinesscodeOrgs")]
        public virtual Businesscode BusinesscodeNavigation { get; set; }
        [ForeignKey(nameof(Orgnr))]
        [InverseProperty(nameof(Organization.BusinesscodeOrgs))]
        public virtual Organization OrgnrNavigation { get; set; }
    }
}
