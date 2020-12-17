using CommonBase.Extensions;
using HungryDogs.Contracts.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HungryDogs.Logic.Time
{
    static class TimeTable
    {
        public static IEnumerable<FromToTime> LoadTimeTable(Entities.Persistence.Restaurant restaurant, DateTime dateTime)
        {
            restaurant.CheckArgument(nameof(restaurant));

            var result = new List<FromToTime>();

            if (restaurant.OpeningHours != null)
            {
                foreach (var item in restaurant.OpeningHours.Where(e => e.Weekday == (int)dateTime.DayOfWeek))
                {
                    result.Add(new FromToTime(item.OpenFrom, item.OpenTo));
                }
            }
            if (restaurant.SepcialOpeningHours != null)
            {
                foreach (var item in restaurant.SepcialOpeningHours.Where(e => (e.From == null && e.To == null)
                                                                            || (e.From == null && dateTime.ToDateSecondStamp() <= e.To.Value.ToDateSecondStamp())
                                                                            || (e.From.Value.ToDateSecondStamp() >= dateTime.ToDateSecondStamp() && e.To == null)))
                {
                    result.Add(new FromToTime(item.From, item.To, item.State));
                }
            }
            return CleanUp(result);
        }
        public static IEnumerable<FromToTime> CleanUp(IEnumerable<FromToTime> timeTable)
        {
            timeTable.CheckArgument(nameof(timeTable));

            var result = new List<FromToTime>();
            var source = new List<FromToTime>(timeTable.OrderBy(e => e.From.ToDateSecondStamp()));
            var entry = source.FirstOrDefault();
            var now = DateTime.Now;

            if (entry == null)
            {
                // The whole day is closed
                var from = new DateTime(now.Year, now.Month, now.Second, 0, 0, 0);
                var to = new DateTime(now.Year, now.Month, now.Second, 23, 59, 59);

                result.Add(new FromToTime(from, to, SpecialOpenState.Closed));
            }
            else if (entry.From.ToTimeSecondStamp() > 0)
            {
                // Fill from 00:00:00 to first entry with a closed entry 
                var from = new DateTime(now.Year, now.Month, now.Second, 0, 0, 0);
                var to = entry.To.AddSeconds(-1);

                result.Add(new FromToTime(from, to, SpecialOpenState.Closed));
            }

            var prv = default(FromToTime);

            foreach (var item in source)
            {
                if (prv != null && item.From.ToDateSecondStamp() - prv.To.ToDateSecondStamp() > 1)
                {
                    // Fill the space betweens the entries
                    result.Add(new FromToTime(prv.To.AddSeconds(1), item.From.AddSeconds(-1), SpecialOpenState.Closed));
                }
                prv = item;
            }            

            entry = source.LastOrDefault();
            if (entry != null && entry.To.ToTimeSecondStamp() < 235959)
            {
                var from = entry.To.AddSeconds(1);
                var to = new DateTime(from.Year, from.Month, from.Second, 23, 59, 59);

                result.Add(new FromToTime(from, to, SpecialOpenState.Closed));
            }
            return result;
        }
        public static FromToTime Create(IEnumerable<FromToTime> timeTable, DateTime now, SpecialOpenState openState)
        {
            timeTable.CheckArgument(nameof(timeTable));

            var result = default(FromToTime);
            var timeList = new List<FromToTime>(timeTable);
            var index = timeList.FindIndex(e => e.IsBetween(now) && e.State != openState);

            if (index > -1)
            {
                var entry = timeList[index];

                result = new FromToTime(now, entry.To, openState);
            }
            else
            {
                result = new FromToTime(now, new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), openState);
            }
            return result;
        }
        public static IEnumerable<FromToTime> Insert(IEnumerable<FromToTime> timeTable, FromToTime fromToTime)
        {
            timeTable.CheckArgument(nameof(timeTable));
            fromToTime.CheckArgument(nameof(fromToTime));

            var timeList = new List<FromToTime>(timeTable);

            return timeList;
        }
    }
}
