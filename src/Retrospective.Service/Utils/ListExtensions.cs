using System;
using System.Collections.Generic;

namespace Retrospective.Service.Utils
{
    public static class ListExtensions
    {
        public static bool NotNullAndTrueForAll(this List<object> thelist, Predicate<object> match)
        {
            return thelist != null && thelist.TrueForAll(match);
        }
    }
}