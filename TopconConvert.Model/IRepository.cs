using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopconConvert.Model
{
    public interface IRepository
    {
        TopconJob LoadJob();
    }
}
