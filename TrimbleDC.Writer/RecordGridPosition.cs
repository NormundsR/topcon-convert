using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrimbleDC.Writer
{
    class RecordGridPosition : Record
    {
        public RecordGridPosition
        (
            string Point,
            double Northing,
            double Easting,
            double Height,
            string Code,
            bool isControl
        )
            : base("69", "FD")
        {
            Append(Point);
            Append(Northing);
            Append(Easting);
            Append(Height);
            Append(Code);
            Append('1');
            Append(isControl ? '2' : '1');
            NewLine();
        }

        public RecordGridPosition
        (
            string derivation,
            string Point,
            double Northing,
            double Easting,
            double Height,
            string Code,
            bool isControl
        )
            : base("69", derivation)
        {
            Append(Point);
            Append(Northing);
            Append(Easting);
            Append(Height);
            Append(Code);
            Append('1');
            Append(isControl ? '2' : '1');
            NewLine();
        }
    }
}
