// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System.Collections.ObjectModel;

namespace BLS.JSON.Utilities
{
    internal class EnumValues<T> : KeyedCollection<string, EnumValue<T>> where T : struct
    {
        protected override string GetKeyForItem(EnumValue<T> item)
        {
            return item.Name;
        }
    }
}