using FluentNHibernate.Mapping;
using NhibernateTest.Entities;
using NhibernateTest.UserTypes;

namespace NhibernateTest.ClassMaps
{
    public class ProductClassMap : ClassMap<Product>
    {
        public ProductClassMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            //Component(x => x.ValidityPeriod).ColumnPrefix("Valid");
            Component(x => x.ValidityPeriod, m =>
            {
                m.Map(x => x.From).Column("ValidFrom").CustomType<CustomDateTypeUserType>();
                m.Map(x => x.Until).Column("ValidUntil").CustomType<CustomDateTypeUserType>();
            });
        }
    }
}
