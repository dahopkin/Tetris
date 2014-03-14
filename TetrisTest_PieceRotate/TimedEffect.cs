using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class TimedEffect
    {

        private int duration;
        //private int lockDelaySecondsRun = 0;
        private DateTime effectStartTime;
        private bool active;
        public bool Active { get { return active; } }
        private TimeUnit timeUnit;
        public TimedEffect(int duration, TimeUnit timeUnit) {
            this.duration = duration;
            this.timeUnit = timeUnit;
            active = false;
        }

        public void Start()
        {
            if (!active)
            {
                active = true;
                effectStartTime = DateTime.Now;
            }
            else return;
        }

        public void ReStart()
        {
            active = false;
            Start();
        }

        public bool Expired()
        {
            if (active)
            {
                DateTime effectCurrentTime = DateTime.Now;
                TimeSpan timeElapsed = effectCurrentTime - effectStartTime;
                if ((timeUnit == TimeUnit.Milliseconds && timeElapsed.Milliseconds >= duration)
                    || (timeUnit == TimeUnit.Seconds && timeElapsed.Seconds >= duration))
                {
                    active = false;
                    return true;
                }
                else return false;
            }
            return false;
        }
    }
}
