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
    public partial class Businesscode
    {
        public Businesscode()
        {
            BusinesscodeOrgs = new HashSet<BusinesscodeOrg>();
            BusinesscodeSuborgs = new HashSet<BusinesscodeSuborg>();
        }

        [Key]
        [Column("businesscode")]
        [StringLength(255)]
        public string Businesscode1 { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty(nameof(BusinesscodeOrg.BusinesscodeNavigation))]
        public virtual ICollection<BusinesscodeOrg> BusinesscodeOrgs { get; set; }
        [InverseProperty(nameof(BusinesscodeSuborg.BusinesscodeNavigation))]
        public virtual ICollection<BusinesscodeSuborg> BusinesscodeSuborgs { get; set; }
    }
}
