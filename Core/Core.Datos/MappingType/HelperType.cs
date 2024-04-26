
using System;
using System.Collections.Generic;
namespace Core.Datos.MappingType
{
    /// <summary>
    /// CoolStorage.NET - .NET Object Relational Mapping Library
    /// </summary>
    public static class CSHelper
    {
        private static int _currentTableAlias;
        private static int _currentFieldAlias;
        private static readonly string[] _tableAliases;
        private static readonly string[] _reservedWords = new string[] { "is", "as", "in", "on", "to", "at", "go", "by", "of", "or", "if", "no" };

        static CSHelper()
        {
            List<string> aliasList = new List<string>();

            for (char c1 = 'a'; c1 < 'z'; c1++)
            {
                for (char c2 = 'a'; c2 < 'z'; c2++)
                {
                    string alias = c1.ToString() + c2;

                    foreach (string reservedWord in _reservedWords)
                        if (alias == reservedWord)
                            alias = "";

                    if (alias.Length > 0)
                        aliasList.Add(alias);
                }
            }

            _tableAliases = aliasList.ToArray();

            _currentTableAlias = 0;
            _currentFieldAlias = 0;
        }

        internal static string NextTableAlias
        {
            get
            {
                lock (_tableAliases)
                {
                    _currentTableAlias = _currentTableAlias % _tableAliases.Length;

                    return _tableAliases[_currentTableAlias++];
                }
            }
        }

        internal static string NextFieldAlias
        {
            get
            {
                lock (_tableAliases)
                {
                    _currentFieldAlias = _currentFieldAlias % _tableAliases.Length;

                    return "f" + _tableAliases[_currentFieldAlias++];
                }
            }
        }

        internal static string GetQueryExpression<T>()
        {
            return GetQueryExpression(typeof(T));
        }

        internal static string GetQueryExpression(Type type)
        {
            if (type.IsDefined(typeof(QueryExpressionAttribute), true))
            {
                QueryExpressionAttribute[] attributes = (QueryExpressionAttribute[])type.GetCustomAttributes(typeof(QueryExpressionAttribute), true);

                return attributes[0].Query;
            }

            throw new CSException("Class " + type.Name + " has no [QueryExpression] attribute");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static object ConvertType(object value, Type targetType, object nullValue)
        {
            if (value != null)
                return ConvertType(value, targetType);

            if (targetType.IsValueType)
            {
                if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return null;

                return nullValue;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object ConvertType(object value, Type targetType)
        {
            if (value == null)
                return null;

            if (value.GetType() == targetType)
                return value;

            if (targetType.IsValueType)
            {
                if (!targetType.IsGenericType)
                {
                    if (targetType.IsEnum)
                        return Enum.ToObject(targetType, value);
                    else
                        return Convert.ChangeType(value, targetType);
                }

                if (targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Type underlyingType = targetType.GetGenericArguments()[0];

                    return ConvertType(value, underlyingType);
                }
            }

            if (targetType.IsAssignableFrom(value.GetType()))
                return value;
            else
                return Convert.ChangeType(value, targetType);
        }
    }
}
