using System;
using System.Collections.Generic;

namespace WOD.Game.Server.Core
{
    public class ScheduledItem : IDisposable
    {
        private readonly Action task;

        public double ExecutionTime { get; private set; }
        public bool Disposed { get;  set; }

        public readonly bool Repeating;
        public readonly double Schedule;
        public readonly string Identifier;

        public ScheduledItem(Action task, double executionTime, string identifier)
        {
            this.task = task;
            ExecutionTime = executionTime;
            Repeating = false;
            Identifier = identifier;

        }

        public ScheduledItem(Action task, double executionTime, double schedule, string identifier)
        {
            this.task = task;
            ExecutionTime = executionTime;
            Schedule = schedule;
            Repeating = true;
            Identifier = identifier;
        }

        public void Reschedule(double newTime)
        {
            ExecutionTime = newTime;
        }

        public void Execute()
        {
            task();
        }

        public void Dispose()
        {
            Scheduler.Unschedule(this);
        }

        public sealed class SortedByExecutionTime : IComparer<ScheduledItem>
        {
            public int Compare(ScheduledItem x, ScheduledItem y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (ReferenceEquals(null, y))
                {
                    return 1;
                }

                if (ReferenceEquals(null, x))
                {
                    return -1;
                }

                return x.ExecutionTime.CompareTo(y.ExecutionTime);
            }
        }
    }
}