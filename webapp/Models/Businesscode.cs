using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace webapp.Models
{
    public partial class Businesscode
    {
        public Businesscode()
        {
            Organizations = new HashSet<Organization>();
            Suborganizations = new HashSet<Suborganization>();
        }

        [Key]
        public string Businesscode1 { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<Suborganization> Suborganizations { get; set; }
    }
}
