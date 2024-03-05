using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static class DateTimeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime)
        {
            if (dateTime.Kind.IsNoneOf(DateTimeKind.Utc, DateTimeKind.Local))
                throw new ArgumentException($"The value of the {nameof(dateTime.Kind)} property of {nameof(dateTime)} must not be 'DateTimeKind.Unspecified'.");

            return new DateTimeOffset(dateTime).ToUniversalTime();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToDateTime(this DateTimeOffset dateTimeOffset, DateTimeKind kind = DateTimeKind.Utc)
            => kind switch
            {
                DateTimeKind.Utc => dateTimeOffset.UtcDateTime,
                DateTimeKind.Local => new DateTime(dateTimeOffset.ToLocalTime().Ticks, DateTimeKind.Local),
                _ => throw new ArgumentException($"The value of the {nameof(kind)} must not be 'DateTimeKind.Unspecified'."),
            };

        public static DateTime? TryToDateTime(this (UInt16 dosDate, UInt16 dosTime) dosDateTimeValue)
        {
            try
            {
                if (dosDateTimeValue.dosDate == 0 && dosDateTimeValue.dosTime == 0)
                    return null;
                return FromDosDateTimeToDateTime(dosDateTimeValue, nameof(dosDateTimeValue)).ToUniversalTime();
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime ToDateTime(this (UInt16 dosDate, UInt16 dosTime) dosDateTimeValue)
            => FromDosDateTimeToDateTime(dosDateTimeValue, nameof(dosDateTimeValue)).ToUniversalTime();

        public static DateTimeOffset? TryToDateTimeOffset(this (UInt16 dosDate, UInt16 dosTime) dosDateTimeValue)
        {
            try
            {
                if (dosDateTimeValue.dosDate == 0 && dosDateTimeValue.dosTime == 0)
                    return null;
                return new DateTimeOffset(FromDosDateTimeToDateTime(dosDateTimeValue, nameof(dosDateTimeValue))).ToUniversalTime();
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTimeOffset ToDateTimeOffset(this (UInt16 dosDate, UInt16 dosTime) dosDateTimeValue)
            => FromDosDateTimeToDateTime(dosDateTimeValue, nameof(dosDateTimeValue)).ToDateTimeOffset().ToUniversalTime();

        public static (UInt16 dosDate, UInt16 dosTime) TryToDosDateTime(this DateTime dateTime)
        {
            if (dateTime.Kind.IsNoneOf(DateTimeKind.Utc, DateTimeKind.Local))
                throw new ArgumentException($"The value of the {nameof(dateTime.Kind)} property of {nameof(dateTime)} must not be 'DateTimeKind.Unspecified'.");

            try
            {
                return FromDateTimeToDosDateTime(dateTime.ToLocalTime(), nameof(dateTime));
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception)
            {
                return (0, 0);
            }
        }

        public static (UInt16 dosDate, UInt16 dosTime) ToDosDateTime(this DateTime dateTime)
        {
            if (dateTime.Kind.IsNoneOf(DateTimeKind.Utc, DateTimeKind.Local))
                throw new ArgumentException($"The value of the {nameof(dateTime.Kind)} property of {nameof(dateTime)} must not be 'DateTimeKind.Unspecified'.");

            return FromDateTimeToDosDateTime(dateTime.ToLocalTime(), nameof(dateTime));
        }

        public static (UInt16 dosDate, UInt16 dosTime) TryToDosDateTime(this DateTimeOffset dateTimeOffset)
        {
            try
            {
                return FromDateTimeToDosDateTime(dateTimeOffset.ToDateTime(DateTimeKind.Local), nameof(dateTimeOffset));
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception)
            {
                return (0, 0);
            }
        }

        public static (UInt16 dosDate, UInt16 dosTime) ToDosDateTime(this DateTimeOffset dateTimeOffset)
            => FromDateTimeToDosDateTime(dateTimeOffset.ToDateTime(DateTimeKind.Local), nameof(dateTimeOffset));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static DateTime FromDosDateTimeToDateTime((UInt16 dosDate, UInt16 dosTime) dosDateTimeValue, String nameOfParameter)
        {
            var unsignedDosTime = (UInt32)dosDateTimeValue.dosTime;
            var unsignedDosDate = (UInt32)dosDateTimeValue.dosDate;
            var second = (Int32)((unsignedDosTime & 0x1f) << 1);
            if (second > 59)
                throw new ArgumentException($"The value for \"{nameof(second)}\" is out of range. : {nameof(second)}={second}", nameOfParameter);

            var minute = (Int32)(unsignedDosTime >> 5 & 0x3f);
            if (minute > 59)
                throw new ArgumentException($"The value for \"{nameof(minute)}\" is out of range. : {nameof(minute)}={minute}", nameOfParameter);

            var hour = (Int32)(unsignedDosTime >> 11 & 0x1f);
            if (hour > 23)
                throw new ArgumentException($"The value for \"{nameof(hour)}\" is out of range. : {nameof(hour)}={hour}", nameOfParameter);

            var month = (Int32)(unsignedDosDate >> 5 & 0xf);
            if (month is < 1 or > 12)
                throw new ArgumentException($"The value for \"{nameof(month)}\" is out of range. : {nameof(month)}={month}", nameOfParameter);

            var year = (Int32)(unsignedDosDate >> 9 & 0x7f) + 1980;

            var day = (Int32)(unsignedDosDate & 0x1f);
            if (day < 1)
                throw new ArgumentException($"The value for \"{nameof(day)}\" is out of range. : {nameof(day)}={day}", nameOfParameter);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            if (day > daysInMonth)
                throw new ArgumentException($"Invalid date. : {year}-{month:D2}-{day:D2}", nameOfParameter);

            return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (UInt16 dosDate, UInt16 dosTime) FromDateTimeToDosDateTime(this DateTime dateTime, String nameOfParameter)
        {
            Validation.Assert(dateTime.Kind == DateTimeKind.Local, "dateTime.Kind == DateTimeKind.Local");

            // 1980年から2107年までの間ではない場合はエラー
            if (!dateTime.Year.IsBetween(1980, 1980 + (Int32)unchecked(((UInt32)(-1) << 25) >> 25)))
                throw new ArgumentOutOfRangeException(nameOfParameter);

            checked
            {
                var date =
                    (((UInt32)dateTime.Year - 1980U) << (25 - 16))
                    | ((UInt32)dateTime.Month << (21 - 16))
                    | ((UInt32)dateTime.Day << (16 - 16));

                var time =
                    ((UInt32)dateTime.Hour << 11)
                    | ((UInt32)dateTime.Minute << 5)
                    | ((UInt32)dateTime.Second >> 1);

                return ((UInt16)date, (UInt16)time);
            }
        }
    }
}
