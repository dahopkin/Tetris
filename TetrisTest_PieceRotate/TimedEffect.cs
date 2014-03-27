/*
 * The TimedEffect class starts and keeps track of a time span, so that a class using it can do
 * something over a certain period of time. When the time has expired, the effect stops. 
 * It can be reset, then started again, if the user/client class wants to use one for the whole program.
 * It needs a number (for time) and a TimeUnit enum (to represent the unit of time) for its creation.
 * The TimedEffect has public methods for starting it, resetting it, and checking to see 
 * if it's active or expired. 
 *
 * (c) Copyright 2014 Daniel Hopkins. All Rights Reserved.
 * E-mail: dahopkin2@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class TimedEffect
    {

        private int duration;
        private DateTime effectStartTime;
        private bool active;
        private bool expired;

        /// <summary>
        /// Gets a boolean indicating if the timed effect is active and hasn't expired.
        /// </summary>
        public bool Active { get { return (active && !expired); } }

        private TimeUnit timeUnit;

        /// <summary>
        /// Instantiates an instance of the TimedEffect class from a number for duration and a TimeUnit enum.
        /// </summary>
        /// <param name="duration">The amount of time the effect runs for.</param>
        /// <param name="timeUnit">The timed unit representing to put the duration in context.</param>
        public TimedEffect(int duration, TimeUnit timeUnit) {
            this.duration = duration;
            this.timeUnit = timeUnit;
            active = false;
            expired = false;
        }

        /// <summary>
        /// This method starts the timed effect (if it's not already active).
        /// </summary>
        public void Start()
        {
            if (!active)
            {
                active = true;
                effectStartTime = DateTime.Now;
            }
            else return;
        }

        /// <summary>
        /// This method resets the timed effect so it can be started again.
        /// </summary>
        public void Reset()
        {
            active = false;
            expired = false;
        }

        /// <summary>
        /// This method runs the timed effect and checks to see if its time has run out.
        /// </summary>
        /// <returns>A boolean indicating whether the timed effect has expired.</returns>
        public bool Expired()
        {
            
            if (expired) return true;
            else
            {
                DateTime effectCurrentTime = DateTime.Now;
                TimeSpan timeElapsed = effectCurrentTime - effectStartTime;
                if ((timeUnit == TimeUnit.Milliseconds && timeElapsed.Milliseconds >= duration)
                    || (timeUnit == TimeUnit.Seconds && timeElapsed.Seconds >= duration))
                {
                    active = false;
                    expired = true;
                    return true;
                }
                else return false;
            }
        }
    }
}
