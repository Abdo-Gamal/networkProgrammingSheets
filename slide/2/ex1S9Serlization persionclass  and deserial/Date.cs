using System;
namespace ex1S9Serlization_persionclass__and_deserial
{
    [Serializable]
    public class Date
    {
        private ushort year;
        private byte month, day;
        private DayOfWeek nameOfDay;
        public Date(int year, int month, int day)
        {
            this.year = (ushort)year;
            this.month = (byte)month;
            this.day = (byte)day;
            this.nameOfDay = (new DateTime(year, month, day)).DayOfWeek;
        }
        public Date(Date d)
        {
            this.year = d.year; this.month = d.month;
            this.day = d.day; this.nameOfDay = d.nameOfDay;
        }
        public int Year { get { return year; } }
        public int Month { get { return month; } }
        public int Day { get { return day; } }
        // return this minus other, as of usual birthday calculations.
        public int YearDiff(Date other)
        {
            if (this.Equals(other))
                return 0;
            else if ((new Date(this.year, this.month,
            this.day)).IsBefore(other))
                return 0;
            else return this.year - other.year; 
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            else if (this.GetType() != obj.GetType())
                return false;
            else if (ReferenceEquals(this, obj))
                return true;
            else if (this.year == ((Date)obj).year &&
            this.month == ((Date)obj).month &&
            this.day == ((Date)obj).day)
                return true;
            else return false;
        }
        public bool IsBefore(Date other)
        {
            return
            this.year < other.year ||
            this.year == other.year && this.month < other.month ||
            this.year == other.year && this.month == other.month && this.day < other.day;
        }
        public static Date Today
        {
            get
            {
                DateTime now = DateTime.Now;
                return new Date(now.Year, now.Month, now.Day);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}.{2}.{3}", nameOfDay, day, month, year);
        }

    }
}