using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace iAsset.Services.DTO
{

    [XmlRoot("NewDataSet")]
    public class CityResult
    {
        [XmlElement("Table")]
        public List<Table> Tables { get; set; }
    }


    public class Table
    {
        [XmlElement("Country")]
        public string Country { get; set; }

        [XmlElement("City")]
        public string City { get; set; }
    }
}
