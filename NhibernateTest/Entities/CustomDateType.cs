using System.Text.Json.Serialization;

namespace NhibernateTest.Entities
{
    [Serializable]
    public readonly struct CustomDateType : IComparable<CustomDateType>, IEquatable<CustomDateType>, IFormattable
    {
        /// <summary>OBSOLETE! Gets a <see cref="Date"></see> object that is set to the current date on this computer, according to the local time.</summary>
        /// <returns>An object whose value is the current local date.</returns>
        public static CustomDateType Now => new CustomDateType(DateTime.Now);

        /// <summary>OBSOLETE! Gets a <see cref="Date"></see> object that is set to the current date on this computer, according to the Coordinated Universal Time (UTC).</summary>
        /// <returns>An object whose value is the current UTC date.</returns>
        public static CustomDateType UtcNow => new CustomDateType(DateTime.UtcNow);

        /// <summary>
        /// Represents the smallest possible value of <see cref="Date"/>. This field is read-only.
        /// </summary>
        public static CustomDateType MinValue => new CustomDateType(DateTime.MinValue);

        /// <summary>
        /// Represents the largest possible value of <see cref="Date"/>. This field is read-only.
        /// </summary>
        public static CustomDateType MaxValue => new CustomDateType(DateTime.MaxValue);

        private readonly long _ticks;

        private CustomDateType(long ticks)
        {
            _ticks = new DateTime(ticks, DateTimeKind.Utc).Date.Ticks;
        }

        public CustomDateType(int year, int month, int day)
        {
            var dateTimeResult = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            _ticks = dateTimeResult.Ticks;
        }

        /// <summary>
        /// OBSOLETE!
        /// </summary>
        /// <param name="dateTime"></param>
        public CustomDateType(DateTime dateTime)
        {
            _ticks = dateTime.Date.Ticks;
        }

        /// <summary>
        /// OBSOLETE!
        /// </summary>
        /// <param name="dateTime"></param>
        public CustomDateType(DateTimeOffset dateTime)
        {
            _ticks = dateTime.Date.Ticks;
        }

        /// <summary>Gets the day of the month represented by this instance.</summary>
        /// <returns>The day component, expressed as a value between 1 and 31.</returns>
        public int Day => DateTime.Day;

        /// <summary>Gets the month component of the date represented by this instance.</summary>
        /// <returns>The month component, expressed as a value between 1 and 12.</returns>
        public int Month => DateTime.Month;

        /// <summary>Gets the year component of the date represented by this instance.</summary>
        /// <returns>The year, between 1 and 9999.</returns>
        public int Year => DateTime.Year;

        /// <summary>
        /// OBSOLETE! Get the UTC kind <see cref="DateTime"/> value for this instance, with the time component set to 00:00:00.000.
        /// </summary>
        [JsonIgnore]
        public DateTime DateTime => new DateTime(_ticks, DateTimeKind.Utc);

        /// <summary>
        /// OBSOLETE! Get the UTC kind <see cref="DateTime"/> value for this instance, with the time component set to one tick less than the next day or simply 23:59:59.999.
        /// </summary>
        [JsonIgnore]
        public DateTime DateTimeWithMaxTime => new DateTime(_ticks, DateTimeKind.Utc).AddDays(1).AddTicks(-1);

        /// <summary>Gets the day of the week represented by this instance.</summary>
        /// <returns>An enumerated constant that indicates the day of the week of this <see cref="Date"></see> value.</returns>
        [JsonIgnore]
        public DayOfWeek DayOfWeek => DateTime.DayOfWeek;

        /// <summary>
        /// Gets the day of the year represented by this instance.
        /// </summary>
        /// <returns>The day of the year, expressed as a value between 1 and 366.</returns>
        [JsonIgnore]
        public int DayOfYear => DateTime.DayOfYear;

        #region Compare

        public static int Compare(CustomDateType t1, CustomDateType t2)
        {
            var ticks1 = t1._ticks;
            var ticks2 = t2._ticks;
            if (ticks1 > ticks2)
            {
                return 1;
            }

            if (ticks1 < ticks2)
            {
                return -1;
            }

            return 0;
        }

        public int CompareTo(CustomDateType other)
        {
            return Compare(this, other);
        }

        public bool Equals(CustomDateType other)
        {
            return _ticks == other._ticks;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is CustomDateType date && Equals(date);
        }

        public override int GetHashCode()
        {
            var ticks = _ticks;
            return unchecked((int)ticks) ^ (int)(ticks >> 32);
        }

        #endregion

        public string ToLongDateString()
        {
            return DateTime.ToString("D");
        }

        public string ToShortDateString()
        {
            return DateTime.ToString("d");
        }

        // ReSharper disable once InconsistentNaming
        public string ToSQLString()
        {
            return $"{Year:0000}-{Month:00}-{Day:00}";
        }

        public override string ToString()
        {
            return $"{Year}-{Month:00}-{Day:00}";
        }

        public string ToString(string format)
        {
            return DateTime.ToString(format);
        }

        public string ToString(IFormatProvider provider)
        {
            return DateTime.ToString(provider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return DateTime.ToString(format, formatProvider);
        }

        public CustomDateType Clone()
        {
            return new CustomDateType(Year, Month, Day);
        }

        /// <summary>
        /// Returns a new <see cref="Date"/>, where the given number of days is added to the current Date.
        /// </summary>
        /// <param name="days">The number of days to add to the current Date. A negative value is permitted.</param>
        public CustomDateType AddDays(int days)
        {
            return new CustomDateType(DateTime.AddDays(days));
        }

        /// <summary>
        /// Returns a new <see cref="Date"/>, where the given number of months is added to the current Date.
        /// </summary>
        /// <param name="months">The number of months to add to the current Date. A negative value is permitted.</param>
        public CustomDateType AddMonths(int months)
        {
            return new CustomDateType(DateTime.AddMonths(months));
        }

        /// <summary>
        /// Returns a new <see cref="Date"/>, where the given number of years is added to the current Date.
        /// </summary>
        /// <param name="years">The number of years to add to the current Date. A negative value is permitted.</param>
        public CustomDateType AddYears(int years)
        {
            return new CustomDateType(DateTime.AddYears(years));
        }

        public TimeSpan Subtract(CustomDateType value)
        {
            return new TimeSpan(_ticks - value._ticks);
        }

        public CustomDateType Subtract(TimeSpan value)
        {
            var dateTime = new DateTime(_ticks, DateTimeKind.Utc);
            var dtResult = dateTime.Subtract(value);
            var date = dtResult.Date;
            return new CustomDateType(date.Year, date.Month, date.Day);
        }

        public bool Between(CustomDateType from, CustomDateType? until)
        {
            if (!until.HasValue)
            {
                return from <= this;
            }

            return from <= this && this <= until.Value;
        }

        /// <summary>
        /// Returns a new <see cref="Date"/> for the first day of the month and year of the current Date.
        /// </summary>
        public CustomDateType FirstDayOfOfMonth()
        {
            return new CustomDateType(Year, Month, 1);
        }

        /// <summary>
        /// Returns a new <see cref="Date"/> for the first day of the quarter of the current Date.
        /// </summary>
        public CustomDateType FirstDayOfQuarter()
        {
            var quarterNumber = (Month - 1) / 3 + 1;
            return new CustomDateType(Year, (quarterNumber - 1) * 3 + 1, 1);
        }

        /// <summary>
        /// Returns a new <see cref="Date"/> for the last day of the month and year of the current Date.
        /// </summary>
        public CustomDateType LastDayOfMonth()
        {
            return new CustomDateType(Year, Month, DateTime.DaysInMonth(Year, Month));
        }

        /// <summary>
        /// Returns a new <see cref="Date"/> for the last day of the quarter of the current Date.
        /// </summary>
        public CustomDateType LastDayOfQuarter()
        {
            var quarterNumber = (Month - 1) / 3 + 1;
            return new CustomDateType(Year, quarterNumber * 3, DateTime.DaysInMonth(Year, quarterNumber * 3));
        }

        public bool IsLastDayOfMonth()
        {
            return this == LastDayOfMonth();
        }

        public bool IsFirstMonthOfQuarter()
        {
            return Month % 3 == 1;
        }

        public bool IsLastMonthOfQuarter()
        {
            return Month % 3 == 0;
        }

        public static CustomDateType operator +(CustomDateType d, TimeSpan t)
        {
            var dtResult = d.DateTime + t;
            return new CustomDateType(dtResult.Year, dtResult.Month, dtResult.Day);
        }

        public static CustomDateType operator -(CustomDateType d, TimeSpan t)
        {
            var dtResult = d.DateTime - t;
            return new CustomDateType(dtResult.Year, dtResult.Month, dtResult.Day);
        }

        public static TimeSpan operator -(CustomDateType d1, CustomDateType d2) => new TimeSpan(d1._ticks - d2._ticks);

        public static bool operator ==(CustomDateType date1, CustomDateType date2) => date1._ticks == date2._ticks;

        public static bool operator !=(CustomDateType date1, CustomDateType date2) => date1._ticks != date2._ticks;

        public static bool operator <(CustomDateType date1, CustomDateType date2) => date1._ticks < date2._ticks;

        public static bool operator >(CustomDateType date1, CustomDateType date2) => date1._ticks > date2._ticks;

        public static bool operator <=(CustomDateType date1, CustomDateType date2) => date1._ticks <= date2._ticks;

        public static bool operator >=(CustomDateType date1, CustomDateType date2) => date1._ticks >= date2._ticks;
    }
}
