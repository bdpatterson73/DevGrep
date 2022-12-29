// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System.Collections.ObjectModel;

namespace BLS.JSON.Schema
{
    internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
    {
        protected override string GetKeyForItem(JsonSchemaNode item)
        {
            return item.Id;
        }
    }
}