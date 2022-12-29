// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Collections.Generic;
using System.Threading;

#if NET20
using BLS.JSON.Utilities.LinqBridge;
#endif

namespace BLS.JSON.Utilities
{
    internal class ThreadSafeStore<TKey, TValue>
    {
        private readonly Func<TKey, TValue> _creator;
        private readonly object _lock = new object();
        private Dictionary<TKey, TValue> _store;

        public ThreadSafeStore(Func<TKey, TValue> creator)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");

            _creator = creator;
            _store = new Dictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            TValue value;
            if (!_store.TryGetValue(key, out value))
                return AddValue(key);

            return value;
        }

        private TValue AddValue(TKey key)
        {
            TValue value = _creator(key);

            lock (_lock)
            {
                if (_store == null)
                {
                    _store = new Dictionary<TKey, TValue>();
                    _store[key] = value;
                }
                else
                {
                    // double check locking
                    TValue checkValue;
                    if (_store.TryGetValue(key, out checkValue))
                        return checkValue;

                    Dictionary<TKey, TValue> newStore = new Dictionary<TKey, TValue>(_store);
                    newStore[key] = value;

#if !(NETFX_CORE || PORTABLE)
                    Thread.MemoryBarrier();
#endif
                    _store = newStore;
                }

                return value;
            }
        }
    }
}