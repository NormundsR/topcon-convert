using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TopconConvert.Model
{
    public enum Control
    {
        None,
        Vertical,
        Horizontal,
        Both
    }

    public class Point
    {
        public string PointNumber;
        public double Northing, Easting, OrthoHeight;
        public string Code;
        [XmlElement("Fixed")]
        public Control control;
    }
}
