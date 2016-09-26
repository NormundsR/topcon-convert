using System;
using System.Collections.Generic;
using System.Linq;
using TopconConvert.Model;
using System.Globalization;
using System.Text;

namespace TrimbleDC.Writer
{
    public class TrimbleDCWriter
    {
        const string nl = "\r\n";
        static CultureInfo culture = new CultureInfo("en-US");

        static double rh;

        static string CheckPrismHeight(Observation o)
        {
            // check if reflector height has changed
            if (rh != o.PrismHeight)
            {
                rh = o.PrismHeight;

                Record ph = new Record("77", "NM");
                ph.Append(o.PrismHeight);
                ph.Append(0.0);
                ph.Append(0.0);
                ph.NewLine();
                return ph;
            }
            else
            {
                return "";
            }
        }

        static string ConvertObservation(Observation o, Station s)
        {
            StringBuilder result = new StringBuilder();
            // check type
            switch (o.obsType)
            {
                case ObservationType.Backsight:
                    result.Append(CheckPrismHeight(o));
                    // OBSERVATION record - D9 F1
                    Record ob = new Record("D9", "F1");
                    ob.Append(s.StationPoint);
                    ob.Append(o.PointNumber);
                    ob.Append("");              // Slope distance - not in backsight.
                    ob.Append(o.ZenithAngle);
                    ob.Append(o.HorizontalAngle);
                    ob.Append(o.Code);
                    ob.Append('1');
                    ob.NewLine();
                    result.Append(ob);
                    break;
                case ObservationType.BacksightPoint:
                    // E1 record
                    Record bs = new Record("E1", "NM");
                    bs.Append(s.StationPoint);
                    bs.Append(o.PointNumber);
                    bs.Append("");
                    bs.Append(o.BacksightBearing);
                    bs.Append("");
                    bs.NewLine();
                    result.Append(bs);
                    break;
                case ObservationType.Shotsight:
                    result.Append(CheckPrismHeight(o));
                    // OBSERVATION record - D9 F1 - no standard deviations given
                    result.Append(String.Format(culture, "{0,2}{1,2}{2,16}{3,16}{4,16}{5,16}{6,16}{7,1}",
                        "D9",
                        "F1",
                        s.StationPoint,
                        o.PointNumber,
                        o.SlopeDistance,
                        o.ZenithAngle,
                        o.HorizontalAngle,
                        o.Code,
                        "1") + nl);
                    break;
            }

            return result.ToString();
        }

        public static string ConvertTopconJob(TopconJob job)
        {
            StringBuilder result = new StringBuilder(10000);

            rh = 0.0;    // track prism height

            // HEADER line
            result.Append(new RecordFreeForm(
                "00",
                "NM",
                "SC V10-70",
                1,
                "",             // date and time
                '1',            // angular units, 1 - DMS
                '1',            // distance units, 1 - Metres
                '1',            // pressure units, 1 - mmHg
                '1',            // temperature units, 1 - Celsius
                '1',            // coordinate order, 1 - NEZ
                '1'             // direction of angle, 1 - Clockwise
            ));

            // PROJECT line
            result.Append(new RecordFreeForm(
                "10",
                "NM",
                job.Project.JobName,
                "1",
                "2",
                "2",
                "2",
                "1",
                "1"
            ));

            // ELLIPSOID and PROJECTION lines - LKS92TM
            result.Append(new RecordFreeForm(
                "65",
                "KI",
                6378137.0,
                298.257221538149));
            result.Append(new RecordFreeForm(
                "64",
                "KI",
                '3',            // TM
                24.0,           // Latitude
                0.0,            // Longitude
                0.0,            // Origin height
                -6000000.0,     // Origin north
                500000,         // Origin east
                0.0,            // Origin elevation
                0.9996          // Scale factor
            ));

            // control points
            foreach (Point p in job._points)
            {
                switch (p.control)
                {
                    case Control.Both:
                        result.Append(new RecordGridPosition(
                            "KI",
                            p.PointNumber,
                            p.Northing,
                            p.Easting,
                            p.OrthoHeight,
                            p.Code,
                            true));
                        break;
                    default:
                        break;
                }
            }

            foreach (Station stn in job._stations)
            {
                // GRID POSITION record, FD derivation
                bool isControl = job._points.Find(p => p.PointNumber == stn.StationPoint).control != Control.None;
                result.Append(new RecordGridPosition(
                    "FD",
                    stn.StationPoint,
                    stn.Northing,
                    stn.Easting,
                    stn.OrthoHeight,
                    stn.StationCode,
                    isControl)
                );
                // STATION record
                // assumes station scale factor 1.0, fixed
                result.Append(new RecordFreeForm(
                    "E0",
                    "NM",
                    stn.StationPoint,
                    stn.InstrumentHeight,
                    1.0,
                    '1'
                ));

                foreach (Observation obs in stn._obs)
                {
                    result.Append(ConvertObservation(obs, stn));
                }
            }

            return result.ToString();
        }
    }
}
