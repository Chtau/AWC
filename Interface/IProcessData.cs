using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWC.Public;

namespace AWC.Interface
{
    public interface IProcessData
    {
        event EventHandler<ProcessEventArgs> ProcessRemoved;
        event EventHandler<ProcessEventArgs> ProcessAdded;

        AWC.WindowHandle.HWNDCollection HWNDCol
        { get; set; }
    }
}
