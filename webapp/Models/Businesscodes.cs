using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace webapp.Models
{
    [Table("businesscodes")]
    [Index(nameof(Name), Name = "businesscodes_name_key", IsUnique = true)]
    public partial class Businesscodes
    {
        public Businesscodes()
        {
            BusinesscodeOrg = new HashSet<BusinesscodeOrg>();
            BusinesscodeSuborg = new HashSet<BusinesscodeSuborg>();
        }

        [Key]
        [Column("businesscode")]
        [StringLength(255)]
        public string Businesscode { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty("BusinesscodeNavigation")]
        public virtual ICollection<BusinesscodeOrg> BusinesscodeOrg { get; set; }
        [InverseProperty("BusinesscodeNavigation")]
        public virtual ICollection<BusinesscodeSuborg> BusinesscodeSuborg { get; set; }
    }
}
