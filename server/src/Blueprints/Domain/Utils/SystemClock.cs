using System;

namespace Utils
{
    public static class SystemClock
    {

        private static bool _isOverriden = false;
        private static DateTime _overridenValue;

        public static DateTime Now
        {
            get
            {
                return _isOverriden ? _overridenValue : DateTime.UtcNow;
            }
        }

        public static void Override(DateTime value)
        {
            _isOverriden = true;
            _overridenValue = value;
        }
    }
}