using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TopconConvert.Model
{
    public class Station
    {
        public String StationPoint;
        public string StationCode;
        public double Northing, Easting, OrthoHeight;
        public double InstrumentHeight;

        [XmlElement("Observation")]
        public List<Observation> _obs { get; set; }
    }
}
