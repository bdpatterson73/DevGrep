// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON.Utilities
{
    internal class EnumValue<T> where T : struct
    {
        private readonly string _name;
        private readonly T _value;

        public EnumValue(string name, T value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public T Value
        {
            get { return _value; }
        }
    }
}