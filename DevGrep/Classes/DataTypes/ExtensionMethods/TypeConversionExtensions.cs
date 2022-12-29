﻿#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using DevGrep.Classes.DataTypes.Comparison;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// Extensions converting between types, checking if something is null, etc.
    /// </summary>
    internal static class TypeConversionExtensions
    {
        #region Functions

        #region FormatToString

        /// <summary>
        /// Calls the object's ToString function passing in the formatting
        /// </summary>
        /// <param name="Input">Input object</param>
        /// <param name="Format">Format of the output string</param>
        /// <returns>The formatted string</returns>
        public static string FormatToString(this object Input, string Format)
        {
            if (Input.IsNull())
                return "";
            return !string.IsNullOrEmpty(Format) ? (string) CallMethod("ToString", Input, Format) : Input.ToString();
        }

        #endregion

        #region IsNotDefault

        /// <summary>
        /// Determines if the object is not null
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">The object to check</param>
        /// <param name="EqualityComparer">Equality comparer used to determine if the object is equal to default</param>
        /// <returns>False if it is null, true otherwise</returns>
        public static bool IsNotDefault<T>(this T Object, IEqualityComparer<T> EqualityComparer = null)
        {
            return !Object.IsDefault(EqualityComparer);
        }

        #endregion

        #region IsDefault

        /// <summary>
        /// Determines if the object is null
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">The object to check</param>
        /// <param name="EqualityComparer">Equality comparer used to determine if the object is equal to default</param>
        /// <returns>True if it is null, false otherwise</returns>
        public static bool IsDefault<T>(this T Object, IEqualityComparer<T> EqualityComparer = null)
        {
            return EqualityComparer.NullCheck(new GenericEqualityComparer<T>()).Equals(Object, default(T));
        }

        #endregion

        #region IsNotNull

        /// <summary>
        /// Determines if the object is not null
        /// </summary>
        /// <param name="Object">The object to check</param>
        /// <returns>False if it is null, true otherwise</returns>
        public static bool IsNotNull(this object Object)
        {
            return !Object.IsNull();
        }

        #endregion

        #region IsNull

        /// <summary>
        /// Determines if the object is null
        /// </summary>
        /// <param name="Object">The object to check</param>
        /// <returns>True if it is null, false otherwise</returns>
        public static bool IsNull(this object Object)
        {
            return Object == null || Convert.IsDBNull(Object);
        }

        #endregion

        #region IsNotNullOrEmpty

        /// <summary>
        /// Determines if a list is not null or empty
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="Value">List to check</param>
        /// <returns>True if it is not null or empty, false otherwise</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> Value)
        {
            return !Value.IsNullOrEmpty();
        }

        #endregion

        #region IsNullOrEmpty

        /// <summary>
        /// Determines if a list is null or empty
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="Value">List to check</param>
        /// <returns>True if it is null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> Value)
        {
            return Value.IsNull() || Value.Count() == 0;
        }

        #endregion

        #region NullCheck

        /// <summary>
        /// Does a null check and either returns the default value (if it is null) or the object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to check</param>
        /// <param name="DefaultValue">Default value to return in case it is null</param>
        /// <returns>The default value if it is null, the object otherwise</returns>
        public static T NullCheck<T>(this T Object, T DefaultValue = default(T))
        {
            return Object.IsNull() ? DefaultValue : Object;
        }

        #endregion

        #region ThrowIfDefault

        /// <summary>
        /// Determines if the object is equal to default value and throws an ArgumentNullException if it is
        /// </summary>
        /// <param name="Item">The object to check</param>
        /// <param name="EqualityComparer">Equality comparer used to determine if the object is equal to default</param>
        /// <param name="Name">Name of the argument</param>
        /// <returns>Returns Item</returns>
        public static T ThrowIfDefault<T>(this T Item, string Name, IEqualityComparer<T> EqualityComparer = null)
        {
            if (Item.IsDefault(EqualityComparer))
                throw new ArgumentNullException(Name);
            return Item;
        }

        /// <summary>
        /// Determines if the object is equal to default value and throws the exception that is passed in if it is
        /// </summary>
        /// <param name="Item">The object to check</param>
        /// <param name="EqualityComparer">Equality comparer used to determine if the object is equal to default</param>
        /// <param name="Exception">Exception to throw</param>
        /// <returns>Returns Item</returns>
        public static T ThrowIfDefault<T>(this T Item, Exception Exception, IEqualityComparer<T> EqualityComparer = null)
        {
            if (Item.IsDefault(EqualityComparer))
                throw Exception;
            return Item;
        }

        #endregion

        #region ThrowIfNull

        /// <summary>
        /// Determines if the object is null and throws an ArgumentNullException if it is
        /// </summary>
        /// <param name="Item">The object to check</param>
        /// <param name="Name">Name of the argument</param>
        /// <returns>Returns Item</returns>
        public static T ThrowIfNull<T>(this T Item, string Name)
        {
            if (Item.IsNull())
                throw new ArgumentNullException(Name);
            return Item;
        }

        /// <summary>
        /// Determines if the object is null and throws the exception passed in if it is
        /// </summary>
        /// <param name="Item">The object to check</param>
        /// <param name="Exception">Exception to throw</param>
        /// <returns>Returns Item</returns>
        public static T ThrowIfNull<T>(this T Item, Exception Exception)
        {
            if (Item.IsNull())
                throw Exception;
            return Item;
        }

        #endregion

        #region ThrowIfNullOrEmpty

        /// <summary>
        /// Determines if the IEnumerable is null or empty and throws an ArgumentNullException if it is
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="Item">The object to check</param>
        /// <param name="Name">Name of the argument</param>
        /// <returns>Returns Item</returns>
        public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T> Item, string Name)
        {
            if (Item.IsNullOrEmpty())
                throw new ArgumentNullException(Name);
            return Item;
        }

        /// <summary>
        /// Determines if the IEnumerable is null or empty and throws the exception passed in if it is
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="Item">The object to check</param>
        /// <param name="Exception">Exception to throw</param>
        /// <returns>Returns Item</returns>
        public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T> Item, Exception Exception)
        {
            if (Item.IsNullOrEmpty())
                throw Exception;
            return Item;
        }

        #endregion

        #region ToSQLDbType

        /// <summary>
        /// Converts a .Net type to SQLDbType value
        /// </summary>
        /// <param name="Type">.Net Type</param>
        /// <returns>The corresponding SQLDbType</returns>
        public static SqlDbType ToSQLDbType(this Type Type)
        {
            return Type.ToDbType().ToSqlDbType();
        }

        /// <summary>
        /// Converts a DbType to a SqlDbType
        /// </summary>
        /// <param name="Type">Type to convert</param>
        /// <returns>The corresponding SqlDbType (if it exists)</returns>
        public static SqlDbType ToSqlDbType(this DbType Type)
        {
            var Parameter = new SqlParameter();
            Parameter.DbType = Type;
            return Parameter.SqlDbType;
        }

        #endregion

        #region ToDbType

        /// <summary>
        /// Converts a .Net type to DbType value
        /// </summary>
        /// <param name="Type">.Net Type</param>
        /// <returns>The corresponding DbType</returns>
        public static DbType ToDbType(this Type Type)
        {
            if (Type == typeof (byte)) return DbType.Byte;
            else if (Type == typeof (sbyte)) return DbType.SByte;
            else if (Type == typeof (short)) return DbType.Int16;
            else if (Type == typeof (ushort)) return DbType.UInt16;
            else if (Type == typeof (int)) return DbType.Int32;
            else if (Type == typeof (uint)) return DbType.UInt32;
            else if (Type == typeof (long)) return DbType.Int64;
            else if (Type == typeof (ulong)) return DbType.UInt64;
            else if (Type == typeof (float)) return DbType.Single;
            else if (Type == typeof (double)) return DbType.Double;
            else if (Type == typeof (decimal)) return DbType.Decimal;
            else if (Type == typeof (bool)) return DbType.Boolean;
            else if (Type == typeof (string)) return DbType.String;
            else if (Type == typeof (char)) return DbType.StringFixedLength;
            else if (Type == typeof (Guid)) return DbType.Guid;
            else if (Type == typeof (DateTime)) return DbType.DateTime2;
            else if (Type == typeof (DateTimeOffset)) return DbType.DateTimeOffset;
            else if (Type == typeof (byte[])) return DbType.Binary;
            else if (Type == typeof (byte?)) return DbType.Byte;
            else if (Type == typeof (sbyte?)) return DbType.SByte;
            else if (Type == typeof (short?)) return DbType.Int16;
            else if (Type == typeof (ushort?)) return DbType.UInt16;
            else if (Type == typeof (int?)) return DbType.Int32;
            else if (Type == typeof (uint?)) return DbType.UInt32;
            else if (Type == typeof (long?)) return DbType.Int64;
            else if (Type == typeof (ulong?)) return DbType.UInt64;
            else if (Type == typeof (float?)) return DbType.Single;
            else if (Type == typeof (double?)) return DbType.Double;
            else if (Type == typeof (decimal?)) return DbType.Decimal;
            else if (Type == typeof (bool?)) return DbType.Boolean;
            else if (Type == typeof (char?)) return DbType.StringFixedLength;
            else if (Type == typeof (Guid?)) return DbType.Guid;
            else if (Type == typeof (DateTime?)) return DbType.DateTime2;
            else if (Type == typeof (DateTimeOffset?)) return DbType.DateTimeOffset;
            return DbType.Int32;
        }

        /// <summary>
        /// Converts SqlDbType to DbType
        /// </summary>
        /// <param name="Type">Type to convert</param>
        /// <returns>The corresponding DbType (if it exists)</returns>
        public static DbType ToDbType(this SqlDbType Type)
        {
            var Parameter = new SqlParameter();
            Parameter.SqlDbType = Type;
            return Parameter.DbType;
        }

        #endregion

        #region ToList

        /// <summary>
        /// Attempts to convert the DataTable to a list of objects
        /// </summary>
        /// <typeparam name="T">Type the objects should be in the list</typeparam>
        /// <param name="Data">DataTable to convert</param>
        /// <param name="Creator">Function used to create each object</param>
        /// <returns>The DataTable converted to a list of objects</returns>
        public static List<T> ToList<T>(this DataTable Data, Func<T> Creator = null) where T : new()
        {
            if (Data.IsNull())
                return new List<T>();
            Creator = Creator.NullCheck(() => new T());
            Type TType = typeof (T);
            PropertyInfo[] Properties = TType.GetProperties();
            var Results = new List<T>();
            for (int x = 0; x < Data.Rows.Count; ++x)
            {
                T RowObject = Creator();

                for (int y = 0; y < Data.Columns.Count; ++y)
                {
                    PropertyInfo Property = Properties.FirstOrDefault(z => z.Name == Data.Columns[y].ColumnName);
                    if (!Property.IsNull())
                        Property.SetValue(RowObject, Data.Rows[x][Data.Columns[y]].TryTo(Property.PropertyType, null),
                                          new object[] {});
                }
                Results.Add(RowObject);
            }
            return Results;
        }

        #endregion

        #region ToType

        /// <summary>
        /// Converts a SQLDbType value to .Net type
        /// </summary>
        /// <param name="Type">SqlDbType Type</param>
        /// <returns>The corresponding .Net type</returns>
        public static Type ToType(this SqlDbType Type)
        {
            return Type.ToDbType().ToType();
        }

        /// <summary>
        /// Converts a DbType value to .Net type
        /// </summary>
        /// <param name="Type">DbType</param>
        /// <returns>The corresponding .Net type</returns>
        public static Type ToType(this DbType Type)
        {
            if (Type == DbType.Byte) return typeof (byte);
            else if (Type == DbType.SByte) return typeof (sbyte);
            else if (Type == DbType.Int16) return typeof (short);
            else if (Type == DbType.UInt16) return typeof (ushort);
            else if (Type == DbType.Int32) return typeof (int);
            else if (Type == DbType.UInt32) return typeof (uint);
            else if (Type == DbType.Int64) return typeof (long);
            else if (Type == DbType.UInt64) return typeof (ulong);
            else if (Type == DbType.Single) return typeof (float);
            else if (Type == DbType.Double) return typeof (double);
            else if (Type == DbType.Decimal) return typeof (decimal);
            else if (Type == DbType.Boolean) return typeof (bool);
            else if (Type == DbType.String) return typeof (string);
            else if (Type == DbType.StringFixedLength) return typeof (char);
            else if (Type == DbType.Guid) return typeof (Guid);
            else if (Type == DbType.DateTime2) return typeof (DateTime);
            else if (Type == DbType.DateTime) return typeof (DateTime);
            else if (Type == DbType.DateTimeOffset) return typeof (DateTimeOffset);
            else if (Type == DbType.Binary) return typeof (byte[]);
            return typeof (int);
        }

        #endregion

        #region ToExpando

        /// <summary>
        /// Converts the object to a dynamic object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">The object to convert</param>
        /// <returns>The object as an expando object</returns>
        public static ExpandoObject ToExpando<T>(this T Object)
        {
            var ReturnValue = new ExpandoObject();
            Type TempType = typeof (T);
            foreach (PropertyInfo Property in TempType.GetProperties())
            {
                ((ICollection<KeyValuePair<String, Object>>) ReturnValue).Add(
                    new KeyValuePair<string, object>(Property.Name, Property.GetValue(Object, null)));
            }
            return ReturnValue;
        }

        #endregion

        #region TryTo

        /// <summary>
        /// Attempts to convert the object to another type and returns the value
        /// </summary>
        /// <typeparam name="T">Type to convert from</typeparam>
        /// <typeparam name="R">Return type</typeparam>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">Default value to return if there is an issue or it can't be converted</param>
        /// <returns>The object converted to the other type or the default value if there is an error or can't be converted</returns>
        public static R TryTo<T, R>(this T Object, R DefaultValue = default(R))
        {
            return (R) Object.TryTo(typeof (R), DefaultValue);
        }

        /// <summary>
        /// Converts an expando object to the specified type
        /// </summary>
        /// <typeparam name="R">Type to convert to</typeparam>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">Default value in case it can't convert the expando object</param>
        /// <returns>The object as the specified type</returns>
        public static R TryTo<R>(this ExpandoObject Object, R DefaultValue = default(R))
            where R : class, new()
        {
            try
            {
                var ReturnValue = new R();
                Type TempType = typeof (R);
                foreach (PropertyInfo Property in TempType.GetProperties())
                {
                    if (((IDictionary<String, Object>) Object).ContainsKey(Property.Name))
                    {
                        Property.SetValue(ReturnValue, ((IDictionary<String, Object>) Object)[Property.Name], null);
                    }
                }
                return ReturnValue;
            }
            catch
            {
            }
            return DefaultValue;
        }

        /// <summary>
        /// Attempts to convert the object to another type and returns the value
        /// </summary>
        /// <typeparam name="T">Type to convert from</typeparam>
        /// <param name="ResultType">Result type</param>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">Default value to return if there is an issue or it can't be converted</param>
        /// <returns>The object converted to the other type or the default value if there is an error or can't be converted</returns>
        public static object TryTo<T>(this T Object, Type ResultType, object DefaultValue = null)
        {
            try
            {
                if (Object.IsNull())
                    return DefaultValue;
                if ((Object as string).IsNotNull())
                {
                    var ObjectValue = Object as string;
                    if (ResultType.IsEnum)
                        return Enum.Parse(ResultType, ObjectValue, true);
                    if (ObjectValue.IsNullOrEmpty())
                        return DefaultValue;
                }
                if ((Object as IConvertible).IsNotNull())
                    return Convert.ChangeType(Object, ResultType);
                if (ResultType.IsAssignableFrom(Object.GetType()))
                    return Object;
                TypeConverter Converter = TypeDescriptor.GetConverter(Object.GetType());
                if (Converter.CanConvertTo(ResultType))
                    return Converter.ConvertTo(Object, ResultType);
                if ((Object as string).IsNotNull())
                    return Object.ToString().TryTo(ResultType, DefaultValue);
            }
            catch
            {
            }
            return DefaultValue;
        }

        #endregion

        #endregion

        #region Private Static Functions

        /// <summary>
        /// Calls a method on an object
        /// </summary>
        /// <param name="MethodName">Method name</param>
        /// <param name="Object">Object to call the method on</param>
        /// <param name="InputVariables">(Optional)input variables for the method</param>
        /// <returns>The returned value of the method</returns>
        private static object CallMethod(string MethodName, object Object, params object[] InputVariables)
        {
            if (string.IsNullOrEmpty(MethodName) || Object.IsNull())
                return null;
            Type ObjectType = Object.GetType();
            MethodInfo Method = null;
            if (InputVariables.IsNotNull())
            {
                var MethodInputTypes = new Type[InputVariables.Length];
                for (int x = 0; x < InputVariables.Length; ++x)
                    MethodInputTypes[x] = InputVariables[x].GetType();
                Method = ObjectType.GetMethod(MethodName, MethodInputTypes);
                if (Method != null)
                    return Method.Invoke(Object, InputVariables);
            }
            Method = ObjectType.GetMethod(MethodName);
            return Method.IsNull() ? null : Method.Invoke(Object, null);
        }

        #endregion
    }
}