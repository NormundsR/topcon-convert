using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TopconConvert.Model
{
    [XmlRootAttribute("data")]
    public class TopconJob
    {
        public Project Project;
        public string DistanceUnitIndicator;

        [XmlElement("Point")]
        public List<Point> _points { get; set; }
        [XmlElement("Station")]
        public List<Station> _stations { get; set; }
    }
}
