namespace NhibernateTest.Entities
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ValidityPeriod ValidityPeriod { get; set; }

    }

    public class  ValidityPeriod
    {
        public virtual CustomDateType From { get; set; }
        public virtual CustomDateType? Until { get; set; }
    }    
}
