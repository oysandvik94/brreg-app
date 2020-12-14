using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace webapp.Models
{
    public partial class Organization
    {
        [Key]
        public int Orgnr { get; set; }
        public string Name { get; set; }
        public string Orgtype { get; set; }
        public string Businessaddress { get; set; }
        public string Municipality { get; set; }
        public string Postaddress { get; set; }
        public string Webaddress { get; set; }
        public DateTime? Registrationdate { get; set; }
        public DateTime? Foundationdate { get; set; }
        public string Ceo { get; set; }
        public string Objective { get; set; }
        public string Businesstype { get; set; }
        public string Businesscode { get; set; }
        public string Sectorcode { get; set; }
        public string Registerednotes { get; set; }
        public string Board { get; set; }
        public string Boardleader { get; set; }
        public string Boarddeputy { get; set; }
        public string Boardmembers { get; set; }
        public string Signature { get; set; }
        public string Auditor { get; set; }
        public string Accountant { get; set; }
        public int? Suborganization { get; set; }

        public virtual Businesscode BusinesscodeNavigation { get; set; }
        public virtual Suborganization SuborganizationNavigation { get; set; }
    }
}
