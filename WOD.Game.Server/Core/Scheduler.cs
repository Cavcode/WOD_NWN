using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WOD.Game.Server.Core.Extensions;

namespace WOD.Game.Server.Core
{
    public static class Scheduler
    {
        private static double Time { get; set; }
        private static double DeltaTime { get; set; }

        private static readonly Stopwatch _stopwatch = new Stopwatch();
        private static readonly List<ScheduledItem> _scheduledItems = new List<ScheduledItem>(1024);
        private static readonly IComparer<ScheduledItem> _comparer = new ScheduledItem.SortedByExecutionTime();
        private static ScheduledItem GetSchedule(string scheduleIdentifier)
        {
            var result = from schedule in _scheduledItems
                         where schedule.ScheduleIdentifier == scheduleIdentifier
                         select schedule;
            return (ScheduledItem)result;
        }
        public static ScheduledItem Schedule(Action task, TimeSpan delay, string scheduleIdentifier)
        {
            if (delay < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), $"{nameof(delay)} cannot be < zero.");
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var item = new ScheduledItem(task, Time + delay.TotalSeconds, scheduleIdentifier);
            _scheduledItems.InsertOrdered(item, _comparer);
            return item;
        }

        public static ScheduledItem ScheduleRepeating(Action task, TimeSpan schedule, TimeSpan delay = default, string scheduleIdenitfier = "")
        {
            if (schedule <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), $"{nameof(delay)} cannot be <= zero.");
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var item = new ScheduledItem(task, Time + delay.TotalSeconds + schedule.TotalSeconds, schedule.TotalSeconds, scheduleIdenitfier);
            _scheduledItems.InsertOrdered(item, _comparer);
            return item;
        }

        internal static void Unschedule(string scheduleIdentifier)
        {
            var scheduledItem = GetSchedule(scheduleIdentifier);
            _scheduledItems.Remove(scheduledItem);
        }

        public static void Process()
        {
            ProcessTime();
            ProcessScheduledItems();
        }
        private static void ProcessTime()
        {
            DeltaTime = _stopwatch.Elapsed.TotalSeconds;
            Time += DeltaTime;
            _stopwatch.Restart();
        }

        private static void ProcessScheduledItems()
        {
            int i;
            for (i = 0; i < _scheduledItems.Count; i++)
            {
                var item = _scheduledItems[i];
                if (Time < item.ExecutionTime)
                {
                    break;
                }

                item.Execute();
                if (!item.Repeating)
                {
                    continue;
                }

                item.Reschedule(Time + item.Schedule);
                _scheduledItems.RemoveAt(i);
                _scheduledItems.InsertOrdered(item, _comparer);
                i--;
            }

            if (i > 0)
            {
                _scheduledItems.RemoveRange(0, i);
            }
        }
    }
}
