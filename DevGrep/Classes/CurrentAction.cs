using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGrep.Classes
{
    internal enum CurrentAction
    {
        Nothing,
        Searching,
        DoneSearchingWithResults,
        DoneSearchingWithoutResults,
        SearchItemSelected
    }
}
