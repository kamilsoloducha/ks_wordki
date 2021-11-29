using System;
using Utils;

namespace Cards.Domain
{
    public readonly struct NextRepeat
    {
        public static readonly NextRepeat NullValue = Restore(null);
        public DateTime? Value { get; }

        private NextRepeat(DateTime? value)
        {
            Value = value;
        }

        public static NextRepeat Restore(DateTime? dateTime)
            => dateTime.HasValue
            ? new NextRepeat(dateTime.Value.Date)
            : NextRepeat.NullValue;

        public static NextRepeat Create()
            => new NextRepeat(SystemClock.Now.Date);

        public static bool operator ==(NextRepeat v1, NextRepeat v2) => v1.Value == v2.Value;
        public static bool operator !=(NextRepeat v1, NextRepeat v2) => v1.Value != v2.Value;
        public override bool Equals(object obj) => obj is NextRepeat nextRepeat ? nextRepeat == this : false;
        public override int GetHashCode() => Value.HasValue ? Value.GetHashCode() : 0;

    }
}