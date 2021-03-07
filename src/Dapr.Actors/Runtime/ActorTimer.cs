// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System;
using System.Globalization;
using System.Threading;
using Dapr.Actors.Resources;

namespace Dapr.Actors.Runtime
{
    /// <summary>
    /// Represents the timer set on an Actor.
    /// </summary>
    public class ActorTimer : ActorTimerToken
    {
        private static readonly TimeSpan MiniumPeriod = Timeout.InfiniteTimeSpan;

        /// <summary>
        /// Initializes a new instance of <see cref="ActorReminder" />.
        /// </summary>
        /// <param name="actorType">The actor type.</param>
        /// <param name="actorId">The actor id.</param>
        /// <param name="name">The reminder name.</param>
        /// <param name="timerCallback">The name of the callback associated with the timer.</param>
        /// <param name="data">The state associated with the reminder.</param>
        /// <param name="dueTime">The reminder due time.</param>
        /// <param name="period">The reminder period.</param>
        public ActorTimer(
            string actorType,
            ActorId actorId,
            string name,
            string timerCallback,
            byte[] data,
            TimeSpan dueTime,
            TimeSpan period)
            : base(actorType, actorId, name)
        {
            if (dueTime < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(dueTime), string.Format(
                    CultureInfo.CurrentCulture,
                    SR.TimerArgumentOutOfRange,
                    TimeSpan.Zero.TotalMilliseconds,
                    TimeSpan.MaxValue.TotalMilliseconds));
            }

            if (period < MiniumPeriod)
            {
                throw new ArgumentOutOfRangeException(nameof(period), string.Format(
                    CultureInfo.CurrentCulture,
                    SR.TimerArgumentOutOfRange,
                    MiniumPeriod.TotalMilliseconds,
                    TimeSpan.MaxValue.TotalMilliseconds));
            }

            this.TimerCallback = timerCallback;
            this.Data = data;
            this.DueTime = dueTime;
            this.Period = period;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        [Obsolete("This conctructor does not provide all required data and should not be used.")]
        public ActorTimer(
            string timerName,
            TimerInfo timerInfo)
            : base("", ActorId.CreateRandom(), timerName)
        {
            this.TimerInfo = timerInfo;
        }

        /// <summary>
        /// Timer related information
        /// </summary>
        #pragma warning disable 0618
        public TimerInfo TimerInfo { get; }
        #pragma warning restore 0618


        /// <summary>
        /// Gets the callback name.
        /// </summary>
        public string TimerCallback { get; }

        /// <summary>
        /// Gets the state passed to the callback.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Gets the time when the timer is first due to be invoked.
        /// </summary>
        public TimeSpan DueTime { get; }

        /// <summary>
        /// Gets the time interval at which the timer is invoked periodically.
        /// </summary>
        public TimeSpan Period { get; }
    }
}
