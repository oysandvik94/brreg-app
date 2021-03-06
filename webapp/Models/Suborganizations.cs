﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace webapp.Models
{
    [Table("suborganizations")]
    public partial class Suborganizations
    {
        [Key]
        [Column("suborgnr")]
        public int Suborgnr { get; set; }
        [Column("suborgname")]
        [StringLength(255)]
        public string Suborgname { get; set; }
        [Column("locationaddress")]
        [StringLength(255)]
        public string Locationaddress { get; set; }
        [Column("postaddress")]
        [StringLength(255)]
        public string Postaddress { get; set; }
        [Column("municipality")]
        [StringLength(255)]
        public string Municipality { get; set; }
        [Column("businesscodes")]
        [StringLength(255)]
        public string Businesscodes { get; set; }
        [Column("registerednotes")]
        [StringLength(255)]
        public string Registerednotes { get; set; }
        [Column("parentorgnr")]
        public int? Parentorgnr { get; set; }

        [ForeignKey(nameof(Parentorgnr))]
        [InverseProperty(nameof(Organizations.Suborganizations))]
        public virtual Organizations ParentorgnrNavigation { get; set; }
    }
}
