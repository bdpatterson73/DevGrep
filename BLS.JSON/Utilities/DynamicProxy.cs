// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

#if !(NET35 || NET20 || PORTABLE40)
using System.Collections.Generic;
using System.Dynamic;

namespace BLS.JSON.Utilities
{
    internal class DynamicProxy<T>
    {
        public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
        {
            return new string[0];
        }

        public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TryConvert(T instance, ConvertBinder binder, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
        {
            return false;
        }

        public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
        {
            return false;
        }

        public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TryGetMember(T instance, GetMemberBinder binder, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            return false;
        }

        public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
        {
            return false;
        }

        public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
        {
            return false;
        }

        public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, out object result)
        {
            result = null;
            return false;
        }
    }
}

#endif