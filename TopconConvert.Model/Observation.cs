using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TopconConvert.Model
{
    public enum ObservationType
    {
        BacksightPoint,
        Backsight,
        Shotsight
    }

    public class Observation
    {
        public string PointNumber;
        [XmlElement("ObsType")]
        public ObservationType obsType;
        public string Code;
        public double SlopeDistance;
        public double ZenithAngle;
        public double HorizontalAngle;
        public double BacksightBearing;
        public double PrismHeight;
    }
}
