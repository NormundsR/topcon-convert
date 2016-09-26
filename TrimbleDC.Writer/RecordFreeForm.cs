using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrimbleDC.Writer
{
    class RecordFreeForm : Record
    {
        public RecordFreeForm(string typeCode, string derivation, params object[] list)
            : base(typeCode, derivation)
        {
            foreach (object o in list)
            {
                Append(o);
            }
            NewLine();
        }
    }
}
