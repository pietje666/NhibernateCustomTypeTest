using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using NhibernateTest.Entities;
using System.Data.Common;

namespace NhibernateTest.UserTypes
{

    public class CustomDateTypeUserType : IUserType
    {
        public bool IsMutable { get; } = false;

        public Type ReturnedType => typeof(CustomDateType);

        public SqlType[] SqlTypes { get; } = { new DateSqlType() };

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var dateTime = (DateTime?)NHibernateUtil.Date.NullSafeGet(rs, names, session);
            return dateTime.HasValue ? new CustomDateType(dateTime.Value) : null;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var date = (CustomDateType?)value;
            NHibernateUtil.Date.NullSafeSet(cmd, date?.DateTime, index, session);
        }
        public object DeepCopy(object value)
        {
            var input = (CustomDateType)value;

            return value == null ? (object)null : DoDeepCopy(input);
        }
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public object Replace(object original, object target, object owner)
        {
            return DeepCopy(original);
        }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public virtual CustomDateType DoDeepCopy(CustomDateType value)
        {
            return value;
        }
    }
}
