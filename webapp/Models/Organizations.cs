using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace webapp.Models
{
    [Table("organizations")]
    public partial class Organizations
    {
        public Organizations()
        {
            Suborganizations = new HashSet<Suborganizations>();
        }

        [Key]
        [Column("orgnr")]
        public int Orgnr { get; set; }
        [Column("orgname")]
        [StringLength(255)]
        public string Orgname { get; set; }
        [Column("orgtype")]
        [StringLength(255)]
        public string Orgtype { get; set; }
        [Column("businessaddress")]
        [StringLength(255)]
        public string Businessaddress { get; set; }
        [Column("municipality")]
        [StringLength(255)]
        public string Municipality { get; set; }
        [Column("postaddress")]
        [StringLength(255)]
        public string Postaddress { get; set; }
        [Column("webaddress")]
        [StringLength(255)]
        public string Webaddress { get; set; }
        [Column("registrationdate", TypeName = "date")]
        public DateTime? Registrationdate { get; set; }
        [Column("foundationdate", TypeName = "date")]
        public DateTime? Foundationdate { get; set; }
        [Column("ceo")]
        [StringLength(255)]
        public string Ceo { get; set; }
        [Column("objective")]
        [StringLength(255)]
        public string Objective { get; set; }
        [Column("businesstype")]
        [StringLength(255)]
        public string Businesstype { get; set; }
        [Column("businesscodes")]
        [StringLength(255)]
        public string Businesscodes { get; set; }
        [Column("sectorcodes")]
        [StringLength(255)]
        public string Sectorcodes { get; set; }
        [Column("registerednotes")]
        [StringLength(255)]
        public string Registerednotes { get; set; }
        [Column("board")]
        [StringLength(255)]
        public string Board { get; set; }
        [Column("boardleader")]
        [StringLength(255)]
        public string Boardleader { get; set; }
        [Column("boarddeputy")]
        [StringLength(255)]
        public string Boarddeputy { get; set; }
        [Column("boardmembers")]
        [StringLength(255)]
        public string Boardmembers { get; set; }
        [Column("signature")]
        [StringLength(255)]
        public string Signature { get; set; }
        [Column("auditor")]
        [StringLength(255)]
        public string Auditor { get; set; }
        [Column("accountant")]
        [StringLength(255)]
        public string Accountant { get; set; }
        [Column("note")]
        [StringLength(255)]
        public string Note { get; set; }

        [InverseProperty("ParentorgnrNavigation")]
        public virtual ICollection<Suborganizations> Suborganizations { get; set; }
    }
}
