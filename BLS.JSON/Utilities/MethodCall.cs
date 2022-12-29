// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON.Utilities
{
    internal delegate TResult MethodCall<T, TResult>(T target, params object[] args);
}