using System;
using System.Collections.Generic;

#nullable disable

namespace webapp.Models
{
    public partial class Suborganization
    {
        public Suborganization()
        {
            Organizations = new HashSet<Organization>();
        }

        public int Orgnr { get; set; }
        public string Name { get; set; }
        public string Locationaddress { get; set; }
        public string Postaddress { get; set; }
        public string Municipality { get; set; }
        public string Businesscode { get; set; }
        public string Registerednotes { get; set; }

        public virtual Businesscode BusinesscodeNavigation { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
