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

        private static readonly Stopwatch stopwatch = new Stopwatch();
        private static readonly List<ScheduledItem> scheduledItems = new List<ScheduledItem>(1024);
        private static readonly IComparer<ScheduledItem> comparer = new ScheduledItem.SortedByExecutionTime();

        public static IDisposable Schedule(Action task, TimeSpan delay, string identifier)
        {
            if (delay < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), $"{nameof(delay)} cannot be < zero.");
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (identifier == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var item = new ScheduledItem(task, Time + delay.TotalSeconds, identifier);
            scheduledItems.InsertOrdered(item, comparer);
            return item;
        }

        public static IDisposable ScheduleRepeating(Action task, TimeSpan schedule, string identifier, TimeSpan delay = default)
        {
            if (schedule <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), $"{nameof(delay)} cannot be <= zero.");
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (identifier == null)
            {
                throw new ArgumentNullException(nameof(task));
            }


            var item = new ScheduledItem(task, Time + delay.TotalSeconds + schedule.TotalSeconds, schedule.TotalSeconds, identifier);
            scheduledItems.InsertOrdered(item, comparer);
            return item;
        }

        public static ScheduledItem GetSchedule(string scheduleName)
        {
            var matchSchedule = scheduledItems;
            for (int i = 0; i <= matchSchedule.Count(); i++)
            {
                if (matchSchedule[i].Identifier == scheduleName)
                {
                    return matchSchedule[i];
                }
            }
            return null;
        }

        internal static void Unschedule(ScheduledItem scheduledItem)
        {
            
            for (int i = 0; i < scheduledItems.Count; i++)
            {
                if (scheduledItems[i].Identifier == scheduledItem.Identifier)
                {
                    scheduledItems[i].Disposed = true;
                    scheduledItems.Remove(scheduledItem);
                }

            }
            Console.WriteLine($"Unscheduled {scheduledItem.Identifier}.");
        }

        public static void Process()
        {
            ProcessTime();
            Update();
        }
        private static void ProcessTime()
        {
            DeltaTime = stopwatch.Elapsed.TotalSeconds;
            Time += DeltaTime;
            stopwatch.Restart();
        }

        public static void Update()
        {
            int i;
            for (i = 0; i < scheduledItems.Count; i++)
            {
                ScheduledItem item = scheduledItems[i];
                if (Time < item.ExecutionTime)
                {
                    break;
                }

                if (item.Disposed)
                {
                    break;
                }


                try
                {
                    if (item.Disposed != true)
                    {
                        item.Execute();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (!item.Repeating || item.Disposed)
                {
                    continue;
                }


                if (item.Disposed == false)
                {
                    item.Reschedule(Time + item.Schedule);
                    Console.WriteLine(item.Identifier);
                    scheduledItems.RemoveAt(i);
                    scheduledItems.InsertOrdered(item, comparer);
                    i--;
                }
            }

            if (i > 0)
            {
                scheduledItems.RemoveRange(0, i);
            }
        }
    }
}
