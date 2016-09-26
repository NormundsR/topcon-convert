using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using TopconConvert.Model;

namespace TopconConvert.Repository
{
    class XMLRepository : IRepository
    {
        //private XDocument document;

        ////TODO: Needs source to load job from.
        //private XMLRepository() {}

        //public XMLRepository(XDocument doc)
        //{
        //    document = doc;
        //}

        public TopconJob LoadJob()
        {
            TopconJob j=new TopconJob();
            
        //    XElement settings = document.Elements("data").Elements("Project").First();
        //    j._Project.name = settings.Element("JobName").Value;


        //    //iterate through points
        //    IEnumerable<XElement> pts=from p in document.Elements("data").Elements("Point")
        //                              select p;

        //    foreach(XElement p in pts)
        //    {
        //        j._points.Add(parsePoint(p));
        //    }

        //    // iterate through stations
        //    IEnumerable<XElement> stns=from s in document.Elements("data").Elements("Station")
        //                              select s;

        //    foreach (XElement stn in stns)
        //    {
        //        j._stations.Add(parseStation(stn));
        //    }
            return j;
        }

        //private Point parsePoint(XElement e)
        //{
        //    Point pnt = new Point();

        //    pnt.name = e.Element("PointNumber").Value;
        //    pnt.e = Double.Parse(e.Element("Easting").Value);
        //    pnt.n = Double.Parse(e.Element("Northing").Value);
        //    pnt.z = Double.Parse(e.Element("Up").Value);
        //    pnt.code = e.Element("Code").Value;

        //    return pnt;
        //}

        //private Station parseStation(XElement e)
        //{
        //    Station stn = new Station();

        //    stn.name = e.Element("StationPoint").Value;
        //    stn.code = e.Element("StationCode").Value;
        //    stn.ih=Double.Parse(e.Element("InstrumentHeight").Value);

        //    IEnumerable<XElement> obs = from o in e.Elements("Observation")
        //                                select o;

        //    foreach (XElement o in obs)
        //    {
        //        stn.obs.Add(parseObservation(o));
        //    }

        //    return stn;
        //}

        //private Observation parseObservation(XElement e)
        //{
        //    Observation o = new ShotsightObs();

        //    o.name = e.Element("PointNumber").Value;
        //    //o.angle = Double.Parse(e.Element("HorizontaAngle").Value);


        //    return o;
        //}
    }
}
