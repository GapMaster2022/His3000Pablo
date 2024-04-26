using System;

namespace Core.Datos.MappingType
{

    /// <summary>
    /// Mapeo hacia los atributos
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class MapToAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public MapToAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public MapToAttribute(string name, string context)
            : this(name)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Context
        {
            get { return _context; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DefaultSortExpressionAttribute : Attribute
    {
        private readonly string _expression;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortExpression"></param>
        public DefaultSortExpressionAttribute(string sortExpression)
        {
            _expression = sortExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Expression
        {
            get { return _expression; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DefaultSortAttribute : Attribute
    {
        private readonly CSSort _sortDirection = CSSort.Ascending;

        /// <summary>
        /// 
        /// </summary>
        public DefaultSortAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortDirection"></param>
        public DefaultSortAttribute(CSSort sortDirection)
        {
            _sortDirection = sortDirection;
        }

        /// <summary>
        /// 
        /// </summary>
        public CSSort SortDirection
        {
            get { return _sortDirection; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SoftDeleteAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TrimAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LazyAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NoCreateAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ToStringAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BooleanValueAttribute : Attribute
    {
        private readonly object _trueValue;
        private readonly object _falseValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        public BooleanValueAttribute(string trueValue, string falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trueValue"></param>
        public BooleanValueAttribute(string trueValue)
        {
            _trueValue = trueValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        public BooleanValueAttribute(int trueValue, int falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trueValue"></param>
        public BooleanValueAttribute(int trueValue)
        {
            _trueValue = trueValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public object TrueValue
        {
            get { return _trueValue; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object FalseValue
        {
            get { return _falseValue; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NullValueAttribute : Attribute
    {
        private readonly object _nullValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        public NullValueAttribute(Int32 i)
        {
            _nullValue = i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        public NullValueAttribute(Double d)
        {
            _nullValue = d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public NullValueAttribute(Boolean b)
        {
            _nullValue = b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public NullValueAttribute(DateTime dt)
        {
            _nullValue = dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public NullValueAttribute(string s)
        {
            _nullValue = s;
        }

        /// <summary>
        /// 
        /// </summary>
        public object NullValue
        {
            get { return _nullValue; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class RelationAttribute : Attribute
    {
        private string _localKey;
        private string _foreignKey;

        /// <summary>
        /// 
        /// </summary>
        public string LocalKey
        {
            get { return _localKey; }
            set { _localKey = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ForeignKey
        {
            get { return _foreignKey; }
            set { _foreignKey = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OneToManyAttribute : RelationAttribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OneToOneAttribute : RelationAttribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ManyToOneAttribute : RelationAttribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SequenceAttribute : Attribute
    {
        private readonly string _sequenceName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequenceName"></param>
        public SequenceAttribute(string sequenceName)
        {
            _sequenceName = sequenceName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SequenceName
        {
            get { return _sequenceName; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ManyToManyAttribute : RelationAttribute
    {
        private string _localLinkKey;
        private string _foreignLinkKey;
        private string _linkTable;
        private bool _pure;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkTable"></param>
        public ManyToManyAttribute(string linkTable)
        {
            _linkTable = linkTable;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Pure
        {
            get { return _pure; }
            set { _pure = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LocalLinkKey
        {
            get { return _localLinkKey; }
            set { _localLinkKey = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ForeignLinkKey
        {
            get { return _foreignLinkKey; }
            set { _foreignLinkKey = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LinkTable
        {
            get { return _linkTable; }
            set { _linkTable = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OptimisticLockAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class QueryExpressionAttribute : Attribute
    {
        private readonly string _query;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        public QueryExpressionAttribute(string sqlQuery)
        {
            _query = sqlQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Query
        {
            get { return _query; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PrefetchAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ServerGeneratedAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ClientGeneratedAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotMappedAttribute : Attribute
    {
    }
}
