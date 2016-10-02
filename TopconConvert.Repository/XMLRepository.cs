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

        public TopconJob LoadJob()
        {
            return new TopconJob();
        }
    }
}
