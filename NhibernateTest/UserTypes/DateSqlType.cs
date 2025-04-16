using NHibernate.SqlTypes;
using System.Data;

namespace NhibernateTest.UserTypes
{
    [Serializable]
    public class DateSqlType : SqlType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateSqlType" /> class.
        /// </summary>
        public DateSqlType()
            : base(DbType.Date)
        {
        }
    }
}
