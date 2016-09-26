using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace TrimbleDC.Writer
{
    class Record
    {
        const string nl = "\r\n";

        string _typeCode;
        string _derivation;
        protected StringBuilder rec;

        static CultureInfo culture = new CultureInfo("en-US");

        private Record() { }

        public Record(string typeCode, string derivation)
        {
            _typeCode = typeCode;
            _derivation = derivation;
            rec = new StringBuilder(_typeCode + _derivation, 80);
        }

        public void Append(string s)
        {
            rec.AppendFormat(culture, "{0,16}", s);
        }

        public void Append(int i)
        {
            rec.AppendFormat(culture, "{0,4}", i);
        }

        public void Append(double d)
        {
            rec.AppendFormat(culture, "{0,16}", d);
        }

        public void Append(char c)
        {
            rec.AppendFormat(culture, "{0,1}", c);
        }

        public void Append(object o)
        {
            if (o is int)
            {
                Append((int)o);
            }
            else if (o is double)
            {
                Append((double)o);
            }
            else if (o is char)
            {
                Append((char)o);
            }
            else if (o is string)
            {
                Append((string)o);
            }
            else
            {
                Append(o.ToString());
            }
        }

        public void NewLine()
        {
            rec.Append(nl);
        }

        public static implicit operator string(Record r)
        {
            return r.ToString();
        }

        new public string ToString()
        {
            return rec.ToString();
        }

        public static implicit operator StringBuilder(Record r)
        {
            return r.rec;
        }
    }
}
