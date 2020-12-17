using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using webapp.Models;
using Xunit;

namespace webapp_tests
{
    public class OrganizationsTest
    {
        [Fact]
        public void SingleSuborg()
        {
            using (var stream = typeof(EmbeddedResource).GetTypeInfo().Assembly.GetManifestResourceStream(namespaceAndFileName))
            // Arrange
            string text = File.ReadAllText("/Resources/test.json");
            JToken token = JToken.Parse(text);
            
            
            var subOrg = new Suborganizations {Suborgnr = 983314635, Suborgname = "AMANDA STORSENTER AS"};
//            var subOrgToken = JToken.Parse(testJson);
            
            var org = new Organizations();
            
            // Act
            org = Organizations.AddSubOrgsFromJson(org, token);
            
            // Assert
            Assert.Equal(1, org.Suborganizations.Count);
            Assert.Equal(subOrg, org.Suborganizations.First());
        }
    }

    public class EmbeddedResource
    {
    }
}