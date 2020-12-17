using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace webapp.Models
{
    public partial class  Organizations
    {
        /**
         * Add suborgs to Organizations object from json 
         */
        public static Organizations AddSubOrgsFromJson(Organizations org, IEnumerable<JToken> subOrgs)
        {
            foreach (var subOrgToken in subOrgs)
            {
                var subOrg = new Suborganizations();
                subOrg.Suborgnr = subOrgToken.SelectToken("organisasjonsnummer").Value<int>();
                subOrg.Suborgname = subOrgToken.SelectToken("navn").Value<string>();
                subOrg.Municipality = subOrgToken.SelectToken("$.beliggenhetsadresse.kommune").Value<string>();
                
                org.Suborganizations.Add(subOrg);
            }

            return org;
        }
    }
}