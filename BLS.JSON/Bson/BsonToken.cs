// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System.Collections;
using System.Collections.Generic;

namespace BLS.JSON.Bson
{
    internal abstract class BsonToken
    {
        public abstract BsonType Type { get; }
        public BsonToken Parent { get; set; }
        public int CalculatedSize { get; set; }
    }

    internal class BsonObject : BsonToken, IEnumerable<BsonProperty>
    {
        private readonly List<BsonProperty> _children = new List<BsonProperty>();

        public override BsonType Type
        {
            get { return BsonType.Object; }
        }

        public IEnumerator<BsonProperty> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string name, BsonToken token)
        {
            _children.Add(new BsonProperty {Name = new BsonString(name, false), Value = token});
            token.Parent = this;
        }
    }

    internal class BsonArray : BsonToken, IEnumerable<BsonToken>
    {
        private readonly List<BsonToken> _children = new List<BsonToken>();

        public override BsonType Type
        {
            get { return BsonType.Array; }
        }

        public IEnumerator<BsonToken> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(BsonToken token)
        {
            _children.Add(token);
            token.Parent = this;
        }
    }

    internal class BsonValue : BsonToken
    {
        private readonly BsonType _type;
        private readonly object _value;

        public BsonValue(object value, BsonType type)
        {
            _value = value;
            _type = type;
        }

        public object Value
        {
            get { return _value; }
        }

        public override BsonType Type
        {
            get { return _type; }
        }
    }

    internal class BsonString : BsonValue
    {
        public BsonString(object value, bool includeLength)
            : base(value, BsonType.String)
        {
            IncludeLength = includeLength;
        }

        public int ByteCount { get; set; }
        public bool IncludeLength { get; set; }
    }

    internal class BsonBinary : BsonValue
    {
        public BsonBinary(byte[] value, BsonBinaryType binaryType)
            : base(value, BsonType.Binary)
        {
            BinaryType = binaryType;
        }

        public BsonBinaryType BinaryType { get; set; }
    }

    internal class BsonRegex : BsonToken
    {
        public BsonRegex(string pattern, string options)
        {
            Pattern = new BsonString(pattern, false);
            Options = new BsonString(options, false);
        }

        public BsonString Pattern { get; set; }
        public BsonString Options { get; set; }

        public override BsonType Type
        {
            get { return BsonType.Regex; }
        }
    }

    internal class BsonProperty
    {
        public BsonString Name { get; set; }
        public BsonToken Value { get; set; }
    }
}