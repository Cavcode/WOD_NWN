﻿using System;
using System.Collections.Generic;

namespace WOD.Game.Server.Core
{
     public class ScheduledItem
    {
        private readonly Action _task;
        public string ScheduleIdentifier { get; set; }

        public double ExecutionTime { get; private set; }

        public readonly bool Repeating;
        public readonly double Schedule;

        public ScheduledItem(Action task, double executionTime, string scheduleIdentifier)
        {
            _task = task;
            ExecutionTime = executionTime;
            Repeating = false;
            ScheduleIdentifier = scheduleIdentifier;
        }

        public ScheduledItem(Action task, double executionTime, double schedule, string scheduleIdentifier)
        {
            _task = task;
            ExecutionTime = executionTime;
            Schedule = schedule;
            Repeating = true;
            ScheduleIdentifier = scheduleIdentifier;
        }

        public void Reschedule(double newTime)
        {
            ExecutionTime = newTime;
        }

        public void Execute()
        {
            _task();
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